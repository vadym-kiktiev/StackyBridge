using UnityEngine;

namespace Logic.Gameplay
{
    public class SpawnArea : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;

        public Vector2 TakeRandomPosition() =>
            (Vector2)transform.position + new Vector2(Random.Range(-_size.x / 2, _size.x / 2), Random.Range(-_size.y / 2, _size.y / 2));

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _size);
        }
    }
}
