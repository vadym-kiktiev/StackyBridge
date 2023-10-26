using DG.Tweening;
using UnityEngine;

namespace Utility
{
    public class AnimationKiller : MonoBehaviour
    {
        private void OnDestroy() =>
            transform.DOKill();
    }
}
