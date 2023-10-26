using System;
using UI.Menu;
using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [Space(5), Header("Configuration")]
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private float raycastDistance = 1f;
        [SerializeField] private LayerMask obstacleLayer;

        [Space(5), Header("Debug")]
        [SerializeField] private bool isMoving = false;
        [SerializeField] private Transform targetPosition;

        public event Action OnTargetReached;

        private Vector2 _moveDirection;
        private Rigidbody2D _rigidbody;

        private Vector2 _randomDirection;

        [SerializeField] private bool _isRed;
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _randomDirection = GetRandomDirection();

            if (_isRed)
                speed = SpeedConfig.RedSpeed;
            else
                speed = SpeedConfig.YellowSpeed;
        }

        private void Update()
        {
            if (isMoving)
            {
                if (!targetPosition)
                {
                    _rigidbody.velocity = Vector2.zero;
                    isMoving = false;

                    targetPosition = null;

                    OnTargetReached?.Invoke();
                    return;
                }

                float horizontalDistance = targetPosition.position.x - transform.position.x;
                float verticalDistance = targetPosition.position.y - transform.position.y;

                Vector2 direction = Vector2.zero;

                Vector2 horizontalDirectionCash = Vector2.zero;
                Vector2 verticalDirectionCash = Vector3.zero;

                horizontalDirectionCash = Vector2.right * Mathf.Sign(horizontalDistance);

                verticalDirectionCash = Vector2.up * Mathf.Sign(verticalDistance);

                if (Mathf.Abs(horizontalDistance) > 0.1f)
                    direction = horizontalDirectionCash;
                else if (Mathf.Abs(verticalDistance) > 0.1f)
                    direction = verticalDirectionCash;

                Vector2 raycastOrigin = (Vector2)transform.position + direction.normalized * Vector2.one;

                RaycastHit2D hit = Physics2D.CircleCast((Vector2)raycastOrigin, 0.2f, direction.normalized, raycastDistance, obstacleLayer);

                if (hit.collider)
                {
                    if (direction == verticalDirectionCash)
                        direction = horizontalDirectionCash;
                    else
                        direction = verticalDirectionCash;

                    raycastOrigin = (Vector2)transform.position + direction.normalized * Vector2.one;
                    
                    RaycastHit2D hitDirection = Physics2D.CircleCast((Vector2)raycastOrigin, 0.2f, direction.normalized, raycastDistance, obstacleLayer);

                    if (hitDirection.collider)
                        direction = GetRandomDirection();
                }

                if (Mathf.Abs(horizontalDistance) < 0.1f && Mathf.Abs(verticalDistance) < 0.1f)
                {
                    isMoving = false;

                    targetPosition = null;

                    OnTargetReached?.Invoke();
                }
                else
                {
                    _rigidbody.velocity = direction * speed;
                }
            }
            else
            {
                Vector2 raycastOrigin = (Vector2)transform.position + _randomDirection.normalized * Vector2.one;

                RaycastHit2D hit = Physics2D.CircleCast((Vector2)raycastOrigin, 0.2f,_randomDirection.normalized, raycastDistance, obstacleLayer);

                if (hit.collider)
                    _randomDirection = GetRandomDirection();

                _rigidbody.velocity = _randomDirection;
            }
        }

        private Vector2 GetRandomDirection()
        {
            if (UnityEngine.Random.Range(0, 100) < 50)
                _moveDirection = Vector2.right * Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));
            else
                _moveDirection = Vector2.up * Mathf.Sign(UnityEngine.Random.Range(-1f, 1f));

            return _moveDirection * speed;
        }

        public void SetTargetPosition(Transform target)
        {
            targetPosition = target;
            isMoving = true;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)transform.position, 0.2f);
        }
#endif
    }
}
