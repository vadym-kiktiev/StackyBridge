using System;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Infrastructure.DIContainer.ProjectInstallers
{
    public class JoystickInputService : MonoBehaviour, IInputService
    {
        [SerializeField] private bl_Joystick _joystick;

        public Vector2 MoveDirection { get; private set; }

        private void Start()
        {
           var canvas = GetComponent<Canvas>();
           canvas.worldCamera = Camera.main;
           canvas.sortingLayerName = "Environment";
        }

        private void Update()
        {
            if (_joystick.Vertical == 0 || _joystick.Horizontal == 0)
                return;

            float v = _joystick.Vertical;
            float h = _joystick.Horizontal;

            Vector2 movement = Vector2.zero;

            movement.x = h;
            movement.y = v;

            movement = movement.normalized;

            if (Math.Abs(movement.x) > Math.Abs(movement.y))
                movement = new Vector2(movement.x, 0);
            else
                movement = new Vector2(0, movement.y);

            MoveDirection = movement;
        }
    }
}
