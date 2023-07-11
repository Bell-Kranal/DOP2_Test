using UnityEngine;

namespace Logic
{
    public class MouseShadowing : MonoBehaviour
    {
        [SerializeField] private SoapAnimation _soap;
        [SerializeField] private float _maxDeltaPosition;

        private bool SoapActive
        {
            set => _soap.gameObject.SetActive(value); 
        }

        private void Update()
        {
            CheckMouseButton();
            MoveToCursor();
        }

        private void MoveToCursor() =>
            transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, _maxDeltaPosition);

        private void CheckMouseButton()
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = Input.mousePosition;
                SoapActive = true;
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    SoapActive = false;
                }
            }
        }
    }
}