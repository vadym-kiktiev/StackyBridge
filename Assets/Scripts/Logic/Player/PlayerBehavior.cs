using System;
using Infrastructure.Services.Score;
using Logic.Bridge;
using Logic.Enemy;
using Logic.Stage;
using Logic.Team;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerBehavior : MonoBehaviour, IBrainBehavior
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private PlayerTail _playerTail;

        [SerializeField] private TeamModel _teamModel;

        private int _currentTailSize = 0;

        [Inject] private INextStageObserverService _nextStageObserverService;
        private IStage _currentStage;
        [Inject] private IStageObserverService _stageObserverService;

        private void Start()
        {
            _stageObserverService.OnAllowStageLoop += StageObserverServiceOnOnAllowStageLoop;
            
            Init(_teamModel);
            _playerTail.Init(_teamModel);
        }

        private void OnDestroy()
        {
            _stageObserverService.OnAllowStageLoop -= StageObserverServiceOnOnAllowStageLoop;
        }

        private void StageObserverServiceOnOnAllowStageLoop()
        {
            _currentStage = _stageObserverService.GetActualStage();
        }

        private void Init(TeamModel teamModel)
        {
            _teamModel = teamModel;

            _spriteRenderer.color = teamModel.TeamColor;
        }

        public void BridgeTrigger(BridgeBehavior bridgeTrigger)
        {
            if (_currentTailSize == 0)
                return;

            if (bridgeTrigger.CheckTeam(_teamModel))
            {
                Debug.Log("Player visit bridge");

                if (_currentTailSize - bridgeTrigger.PartsLeft <= 0)
                {
                    bridgeTrigger.RestoreBridgePart(_currentTailSize);
                    _playerTail.Remove(_currentTailSize);

                    _currentTailSize = 0;
                }
                else
                {
                    _playerTail.Remove(bridgeTrigger.PartsLeft);
                    bridgeTrigger.RestoreBridgePart(bridgeTrigger.PartsLeft);

                    _currentTailSize -= bridgeTrigger.PartsLeft;
                }
            }
        }

        public void NextStage()
        {
            var currStage = _currentStage;

            _currentStage = _stageObserverService.GetNextStage(currStage);

            _nextStageObserverService.NotifyNextStagePlayer(_currentStage, currStage);

            _nextStageObserverService.NotifyNextStage(currStage.Index);
        }

        public bool GrabResource(string teamModel, GameObject grabObject)
        {
            if (_teamModel.TeamId == teamModel)
            {
                _currentTailSize++;

                _playerTail.Add();

                return true;
            }

            return false;
        }
    }
}
