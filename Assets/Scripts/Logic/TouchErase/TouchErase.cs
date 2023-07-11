using System.Collections.Generic;
using UnityEngine;

namespace Logic.TouchErase
{
    public abstract class TouchErase : MonoBehaviour
    {
        [SerializeField] private Texture _mainTexture;
        [SerializeField] private Shader _eraserShader;
    
        private static readonly int ShaderColor = Shader.PropertyToID("_Color");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int Coordinate = Shader.PropertyToID("_Coordinate");
        private static readonly int IntermediateTex = Shader.PropertyToID("_IntermediateTex");

        private RenderTexture _intermediateTex;
        private Material _mainMaterial;
        private Material _eraseMaterial;
        private Camera _camera;
        private Transform _soapTransform;
        private RaycastHit _hit;
        
        protected HashSet<Vector2> UsedTextureCoordinates;

        private void Awake()
        {
            _camera = Camera.main;
            _eraseMaterial = new Material(_eraserShader);
            _eraseMaterial.SetVector(ShaderColor, Color.red);

            _mainMaterial = GetComponent<MeshRenderer>().material;
            _mainMaterial.SetTexture(MainTex, _mainTexture);
            UsedTextureCoordinates = new HashSet<Vector2>();

            ResetRenderTexture();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Erasing(_soapTransform.position);
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    CheckWin();
                }
            }
        }

        public void SetSoapTransform(SoapAnimation soapAnimation) =>
            _soapTransform = soapAnimation.transform;

        protected virtual void CheckWin() { }

        private void Erasing(Vector2 position)
        {
            if(Physics.Raycast(_camera.ScreenPointToRay(position), out _hit))
            {
                if (!_hit.collider.name.Equals(gameObject.name))
                    return;
                RenderTexture.active = _intermediateTex;
                
                _eraseMaterial.SetVector(Coordinate, new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                RenderTexture template = RenderTexture.GetTemporary(_intermediateTex.width, _intermediateTex.height, 0, RenderTextureFormat.ARGBFloat);
            
                Graphics.Blit(_intermediateTex, template);
                Graphics.Blit(template, _intermediateTex, _eraseMaterial);
                RenderTexture.ReleaseTemporary(template);

                UsedTextureCoordinates.Add(new Vector2(float.Parse(_hit.textureCoord.x.ToString("0.0")), float.Parse(_hit.textureCoord.y.ToString("0.0"))));
            }
        }

        protected void ResetRenderTexture() {
            _intermediateTex = new RenderTexture(_mainTexture.width, _mainTexture.height, 0, RenderTextureFormat.ARGBFloat);
            _mainMaterial.SetTexture(IntermediateTex, _intermediateTex);
            RenderTexture.active = _intermediateTex;
        }
    }
}
