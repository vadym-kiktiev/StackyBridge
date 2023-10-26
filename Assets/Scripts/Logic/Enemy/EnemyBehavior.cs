using System;
using Infrastructure.Services.Score;
using Logic.Bridge;
using Logic.Player;
using Logic.Stage;
using Logic.Team;
using UnityEngine;
using Zenject;

namespace Logic.Enemy
{
    public class EnemyBehavior : MonoBehaviour, IBrainBehavior
    {
        [Inject] private IStageObserverService _stageObserverService;

        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private PlayerTail _playerTail;

        [SerializeField] private TeamModel _teamModel;

        [SerializeField] private int _limitTail = 5;

        private int _currentTailSize = 0;

        private IStage _currentStage;
        [Inject] private INextStageObserverService _nextStageObserverService;
        private int _previosStage;

        private void Start()
        {
            _stageObserverService.OnAllowStageLoop += StageObserverServiceOnOnAllowStageLoop;

            _enemyMover.OnTargetReached += ChangeTarget;

            _playerTail.Init(_teamModel);
        }

        private void StageObserverServiceOnOnAllowStageLoop()
        {
            _currentStage = _stageObserverService.GetActualStage();

            ChangeTarget();
        }

        private void OnDestroy()
        {
            _stageObserverService.OnAllowStageLoop -= StageObserverServiceOnOnAllowStageLoop;

            _enemyMover.OnTargetReached -= ChangeTarget;
        }

        private void ChangeTarget()
        {
            if (_currentStage.CanIMoveToNextStage(_teamModel))
            {
                _currentStage.GetNextStage(_teamModel, out Transform nextStage);

                if (nextStage == null)
                    return;

                _previosStage = _currentStage.Index;

                _currentStage = _stageObserverService.GetNextStage(_currentStage);

                MoveTo(nextStage);
            }
            else if (_playerTail.GetTailCount() >= _limitTail)
            {
                MoveToBridge();
            }
            else
            {
                var target = _currentStage.GetTarget(_teamModel, transform.position);

                if (target)
                {
                    MoveTo(target);
                }
                else
                {
                    if (_playerTail.GetTailCount() > 0)
                        MoveToBridge();
                    else
                        _currentStage.OnResourceSpawned += CheckResourceSpawned;
                }
            }
        }

        private void MoveToBridge()
        {
            _currentStage.GetBridgeTarget(_teamModel, out Transform target);

            if (target)
                MoveTo(target);
            else
                throw new AggregateException("Bridge target not found");
        }

        private void CheckResourceSpawned()
        {
            _currentStage.OnResourceSpawned -= CheckResourceSpawned;

            ChangeTarget();
        }

        private void MoveTo(Transform target)
        {
            _enemyMover.SetTargetPosition(target);
        }

        public bool GrabResource(string teamModel, GameObject o)
        {
            if (_teamModel.TeamId == teamModel)
            {
                _currentTailSize++;

                _playerTail.Add();

                return true;
            }

            return false;
        }

        public void BridgeTrigger(BridgeBehavior bridgeBehavior)
        {
            if (_currentTailSize == 0)
                return;

            if (bridgeBehavior.CheckTeam(_teamModel))
            {
                Debug.Log("Player visit bridge");

                if (_currentTailSize - bridgeBehavior.PartsLeft <= 0)
                {
                    bridgeBehavior.RestoreBridgePart(_currentTailSize);
                    _playerTail.Remove(_currentTailSize);

                    _currentTailSize = 0;
                }
                else
                {
                    _playerTail.Remove(bridgeBehavior.PartsLeft);
                    bridgeBehavior.RestoreBridgePart(bridgeBehavior.PartsLeft);

                    _currentTailSize -= bridgeBehavior.PartsLeft;
                }
            }
        }

        public void NextStage()
        {
            _nextStageObserverService.NotifyNextStage(_previosStage);
        }
    }

    public interface IBrainBehavior
    {
        bool GrabResource(string teamModel, GameObject o);
        void BridgeTrigger(BridgeBehavior bridgeBehavior);
        void NextStage();
    }
}
