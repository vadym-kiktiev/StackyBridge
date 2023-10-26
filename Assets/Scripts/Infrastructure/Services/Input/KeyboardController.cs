using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class KeyboardController : MonoBehaviour, IInputService
    {
        public Vector2 MoveDirection { get; private set; }

        void Update()
        {
            float horizontalInput = UnityEngine.Input.GetAxisRaw("Horizontal");
            float verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

            Vector2 movement = Vector2.zero;

            if (horizontalInput != 0)
                movement.x = horizontalInput;
            else if (verticalInput != 0)
                movement.y = verticalInput;

            MoveDirection = movement.normalized;
        }
    }
}
