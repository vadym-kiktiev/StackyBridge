using System;
using DG.Tweening;
using Infrastructure.Services.Score;
using Logic.Stage;
using UnityEngine;
using Zenject;

namespace Logic.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        [Inject] INextStageObserverService _stageObserverService;

        private void Start()
        {
            _stageObserverService.OnNextStagePlayer += MoveCamera;
        }

        private void OnDestroy()
        {
            _stageObserverService.OnNextStagePlayer -= MoveCamera;
        }

        private void MoveCamera(IStage stage, IStage previousStage)
        {
            _cameraTransform.DOMoveY(_cameraTransform.transform.localPosition.y + previousStage.GetSize(), 0.5f);
        }
    }
}
