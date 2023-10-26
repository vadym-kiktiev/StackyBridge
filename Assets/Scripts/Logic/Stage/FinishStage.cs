using Infrastructure.Services.Score;
using Logic.Level;
using Logic.Team;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Logic.Stage
{
    public class FinishStage : StageBase
    {
        [SerializeField] private PlayerTrigger _playerTrigger;

        [Inject] IGameResultService _gameResultService;

        public override void Initialize(BlockConfig teamModels, TeamsConfig teamsConfig)
        {
            _playerTrigger.OnWinTrigger += OnWinTrigger;
            _playerTrigger.OnLoseTrigger += OnLoseTrigger;
        }

        private void OnLoseTrigger() =>
            _gameResultService.Lose();

        private void OnWinTrigger() =>
            _gameResultService.Win();

        public override void StartStage()
        {
        }

        public override void EndStage()
        {
        }

        public override void Tick()
        {
        }
    }
}
