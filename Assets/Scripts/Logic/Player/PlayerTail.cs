using System.Collections.Generic;
using Logic.Team;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerTail : MonoBehaviour
    {
        [SerializeField] private float _circleDiameter;

        [SerializeField] private GameObject _playerTailPrefab;

        private List<Transform> _snakeCircles = new();
        private List<Vector2> _positions = new();

        private TeamModel _teamModel;

        public int GetTailCount() => _snakeCircles.Count;

        private void Awake() =>
            _positions.Add(transform.position);

        public void Init(TeamModel teamModel) =>
            _teamModel = teamModel;

        private void Update()
        {
            float distance = ((Vector2) transform.position - _positions[0]).magnitude;

            if (distance > _circleDiameter)
            {
                Vector2 direction = ((Vector2) transform.position - _positions[0]).normalized;

                _positions.Insert(0, _positions[0] + direction * _circleDiameter);
                _positions.RemoveAt(_positions.Count - 1);

                distance -= _circleDiameter;
            }

            for (int i = 0; i < _snakeCircles.Count; i++)
                _snakeCircles[i].position = Vector2.Lerp(_positions[i + 1], _positions[i], distance / _circleDiameter);
        }

        public void Add()
        {
            Transform circle = Instantiate(_playerTailPrefab, _positions[^1], Quaternion.identity, transform).transform;

            circle.GetComponent<SpriteRenderer>().color = _teamModel.TeamColor;

            _snakeCircles.Add(circle);
            _positions.Add(circle.position);
        }

        public void Remove(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (_snakeCircles.Count == 0)
                    return;

                Destroy(_snakeCircles[^1].gameObject);

                _snakeCircles.RemoveAt(_snakeCircles.Count - 1);
                _positions.RemoveAt(_positions.Count - 1);
            }
        }
    }
}
