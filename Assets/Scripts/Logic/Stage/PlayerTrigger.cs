using System;
using UnityEngine;

namespace Logic.Stage
{
    public class PlayerTrigger : MonoBehaviour
    {
        public event Action OnWinTrigger;
        public event Action OnLoseTrigger;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                OnWinTrigger?.Invoke();
            if(other.CompareTag("Enemy"))
                OnLoseTrigger?.Invoke();
        }
    }
}
