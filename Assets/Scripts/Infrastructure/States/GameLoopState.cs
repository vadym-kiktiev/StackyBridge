using Infrastructure.Factory;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;
using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        private readonly ISpawnPointService _spawnPoint;
        private readonly IGameResetService _gameResetService;
        private readonly IAudioService _audioService;
        private readonly IStageObserverService _stageService;

        private IResourceFactory _resourceFactory;

        public GameLoopState(GameStateMachine gameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine;

            _gameResetService = diContainer.Resolve<IGameResetService>();
            _audioService = diContainer.Resolve<IAudioService>();
            _stageService = diContainer.Resolve<IStageObserverService>();
        }

        public void Enter()
        {
            _gameResetService.OnRestart += ToRestart;

            _audioService.PlayBackground(BackgroundClip.Game);

            _stageService.AllowStageLoop();
        }

        private void ToRestart() =>
            _gameStateMachine.Enter<LoadMenuState>();

        public void Exit()
        {
            _gameResetService.OnRestart -= ToRestart;
        }

        private void ToWinSate()
        {
            _audioService.PlayOneShot(AudioClipShot.Win);
            Debug.LogWarning("Win");
        }

        private void ToLoseState()
        {
            _audioService.PlayOneShot(AudioClipShot.Lose);
            Debug.LogWarning("Lose");
        }
    }
}
