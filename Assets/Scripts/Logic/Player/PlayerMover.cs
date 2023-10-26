using Infrastructure.Services.Input;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerMover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed = 5f;

        [Inject] private IInputService _inputService;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _moveSpeed = SpeedConfig.PlayerSpeed;
        }

        private void Update()
        {
            if (InputConfig.InputType != InputType.Touch)
                Move(_inputService.MoveDirection);
            else
                MoveToPoint(_inputService.MoveDirection);
        }


        private void MoveToPoint(Vector2 inputServiceMoveDirection)
        {
            float horizontalDistance = inputServiceMoveDirection.x - transform.position.x;
            float verticalDistance = inputServiceMoveDirection.y - transform.position.y;

            if(Mathf.Abs(horizontalDistance) < 0.2f && Mathf.Abs(verticalDistance) < 0.2f)
            {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }

            Vector2 direction = Vector2.zero;

            Vector2 horizontalDirectionCash = Vector2.zero;
            Vector2 verticalDirectionCash = Vector3.zero;

            horizontalDirectionCash = Vector2.right * Mathf.Sign(horizontalDistance);

            verticalDirectionCash = Vector2.up * Mathf.Sign(verticalDistance);

            if (Mathf.Abs(horizontalDistance) > 0.05f)
                direction = horizontalDirectionCash;
            else if (Mathf.Abs(verticalDistance) > 0.05f)
                direction = verticalDirectionCash;

            _rigidbody2D.velocity = direction * _moveSpeed;
        }

        private void Move(Vector2 vector)
        {
            _rigidbody2D.velocity = vector * _moveSpeed;
        }
    }
}
