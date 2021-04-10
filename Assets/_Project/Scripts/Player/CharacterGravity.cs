using UnityEngine;

namespace HuntTheMonster.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterGravity : MonoBehaviour
    {
        private CharacterController _controller;
        
        private bool NotGrounded => !_controller.isGrounded;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (NotGrounded)
            {
                ApplyGravity();
            }
        }

        private void ApplyGravity()
        {
            var movement = Physics.gravity * Time.fixedDeltaTime;
            _controller.Move(movement);
        }
    }
}