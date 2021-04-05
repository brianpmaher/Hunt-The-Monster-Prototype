using UnityEngine;

namespace HuntTheMonster
{
    public class MouseLookBehavior : MonoBehaviour
    {
        [SerializeField] private Camera firstPersonCamera;
        [SerializeField] private float verticalLookSpeed = 5f;
        [SerializeField] private float horizontalLookSpeed = 5f;

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            var horizontal = Input.GetAxis("Mouse X") * horizontalLookSpeed * Time.deltaTime;
            var vertical = -Input.GetAxis("Mouse Y") * verticalLookSpeed * Time.deltaTime;
            var cameraRotation = new Vector3(vertical, 0, 0);
            var transformRotation = new Vector3(0, horizontal, 0);
            
            firstPersonCamera.transform.Rotate(cameraRotation);
            transform.Rotate(transformRotation);
        }
    }
}