using UnityEngine;
using UnityEngine.InputSystem;

namespace Lihter
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private float movementSpeed;

        private bool IsMoving { get; set; }

        private Vector2 _direction;

        // Cached animations
        private static readonly int AnimMovementX = Animator.StringToHash("MovementX");
        private static readonly int AnimMovementY = Animator.StringToHash("MovementY");

        private void Update()
        {
            Animate();
        }

        private void FixedUpdate()
        {
            // Apply movement
            rigidbody.velocity = _direction;
            rigidbody.velocity *= IsMoving ? movementSpeed * Time.fixedDeltaTime : 1;
        }

        public void InputActionMove(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
            IsMoving = _direction != Vector2.zero;
        }

        private void Animate()
        {
            if (IsMoving)
            {
                animator.SetFloat(AnimMovementX, _direction.x);
                animator.SetFloat(AnimMovementY, _direction.y);
            }
        }
    }
}
