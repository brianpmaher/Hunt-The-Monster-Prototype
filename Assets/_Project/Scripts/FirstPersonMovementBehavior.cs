using UnityEngine;

namespace HuntTheMonster
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonMovementBehavior : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 2f;
        [SerializeField] private float strafeSpeed = 2f;

        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var z = Input.GetAxis("Vertical") * forwardSpeed * Time.deltaTime;
            var x = Input.GetAxis("Horizontal") * strafeSpeed * Time.deltaTime;
            var localMovement = new Vector3(x, 0, z);
            var movement = transform.TransformDirection(localMovement);
            
            _controller.Move(movement);
        }
    }
}
