using DG.Tweening;
using UnityEngine;

namespace Logic
{
    public class SoapAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _duration;
        
        private void Awake()
        {
            transform
                .DORotate(_rotation, _duration)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}