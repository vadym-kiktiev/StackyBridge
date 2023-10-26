using System;
using Logic.Enemy;
using Logic.Player;
using UnityEngine;

namespace Logic.Bridge
{
    public class BridgeTrigger : MonoBehaviour
    {
        private const string Player = "Player";

        [SerializeField] private BridgeBehavior _bridgeBehavior;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Player) || other.CompareTag("Enemy"))
            {
                if (other.TryGetComponent<IBrainBehavior>(out var player))
                {
                    player.BridgeTrigger(_bridgeBehavior);
                }
            }
        }
    }
}
