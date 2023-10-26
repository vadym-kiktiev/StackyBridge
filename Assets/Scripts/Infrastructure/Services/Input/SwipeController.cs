using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class SwipeController : MonoBehaviour, IInputService
    {
        public float swipeThreshold = 100f;

        public Vector2 MoveDirection { get; private set; }

        private Vector2 touchStartPos;
        private Vector2 touchEndPos;

        void Update()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    touchEndPos = touch.position;

                    float swipeDistance = Vector2.Distance(touchStartPos, touchEndPos);

                    if (swipeDistance > swipeThreshold)
                    {
                        Vector2 swipeDirection = (touchEndPos - touchStartPos).normalized;

                        if (Math.Abs(swipeDirection.x) > Math.Abs(swipeDirection.y))
                            swipeDirection = new Vector2(swipeDirection.x, 0);
                        else
                            swipeDirection = new Vector2(0, swipeDirection.y);

                        MoveDirection = swipeDirection;
                    }
                }
            }
        }

    }
}
