using Infrastructure.Services.Input;
using UnityEngine;

namespace Infrastructure.DIContainer.ProjectInstallers
{
    public class TouchInputService : MonoBehaviour, IInputService
    {
        public Vector2 MoveDirection { get; private set; }

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 realWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                MoveDirection = realWorldPos;
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 realWorldPos = _camera.ScreenToWorldPoint(touch.position);

                MoveDirection = realWorldPos;
            }
#endif
        }
    }
}
