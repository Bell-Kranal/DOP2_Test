using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class WinLevelIcon : MonoBehaviour
    {
        [SerializeField] private float _firstSize;
        [SerializeField] private float _duration;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.one * _firstSize;
            _image.color = new Color(1f, 1f, 1f, 0f);
            
            transform.DOScale(Vector3.one, _duration);
            _image.DOColor(Color.white, _duration);
        }
    }
}