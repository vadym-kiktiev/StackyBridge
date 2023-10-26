using System;
using Logic.Enemy;
using Logic.Team;
using UnityEngine;

namespace Logic.Gameplay
{
    public class GrabObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public event Action<GrabObject> OnGrab;
        public string TeamId => _teamModel;

        [SerializeField] private string _teamModel;

        public void Init(TeamModel teamModel)
        {
            _spriteRenderer.color = teamModel.TeamColor;
            _teamModel = teamModel.TeamId;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                if (other.TryGetComponent<IBrainBehavior>(out var playerBehavior))
                {
                    if (playerBehavior.GrabResource(_teamModel, gameObject))
                    {
                        OnGrab?.Invoke(this);
                    }
                }
            }
        }
    }
}
