using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lihter
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator anim;
        public float movementSpeed;
        private Vector2 _movementInput;

        private void Awake() 
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update() 
        {
            Move();
            Animate();
        }

        private void Move() 
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if(horizontal == 0 && vertical == 0)
            {
                rb.velocity = new Vector2(0,0);
                return;
            }

            _movementInput = new Vector2(horizontal,vertical);
            rb.velocity = _movementInput * movementSpeed * Time.fixedDeltaTime;
        }

        private void Animate() 
        {
            anim.SetFloat("MovementX",_movementInput.x);
            anim.SetFloat("MovementY",_movementInput.y);
        }
    }
}
