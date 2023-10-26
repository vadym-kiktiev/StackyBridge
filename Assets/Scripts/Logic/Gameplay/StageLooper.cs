using System.Collections.Generic;
using Infrastructure.Services.Score;
using Logic.Stage;
using UnityEngine;
using Zenject;

namespace Logic.Gameplay
{
    public class StageLooper : MonoBehaviour
    {
        [Inject] IStageObserverService _stageObserverService;
        [Inject] INextStageObserverService _nextStageObserverService;

        private Queue<IStage> _stages;
        private IStage _actualStage;

        private void Start()
        {
            _stageObserverService.OnAllowStageLoop += InitStages;
            _nextStageObserverService.OnNextStage += NextStage;
        }

        private void OnDestroy()
        {
            _stageObserverService.OnAllowStageLoop -= InitStages;
            _nextStageObserverService.OnNextStage -= NextStage;
        }

        private void InitStages()
        {
            _stages = _stageObserverService.GetStages();

            _actualStage = _stages.Dequeue();
            _actualStage.StartStage();
        }

        private void Update()
        {
            if (_actualStage != null)
                _actualStage.Tick();
        }

        private void NextStage(int stage)
        {
            if (_actualStage.Index > stage)
                return;

            if (_stages.Count == 0)
                return;

            _stageObserverService.StateChanged(_stages.Peek(),_actualStage);

            if (_actualStage != null)
                _actualStage.EndStage();

            _actualStage = _stages.Dequeue();

            _actualStage.StartStage();
        }
    }
}
