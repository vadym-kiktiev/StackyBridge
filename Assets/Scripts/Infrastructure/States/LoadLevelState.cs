using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using Logic.Level;
using Logic.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private IGameStateMachine _stateMachine;

        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IAssets _assetProvider;
        private readonly IStageObserverService _stageService;

        private LevelConfig _config;

        public LoadLevelState(GameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;

            _assetProvider = diContainer.Resolve<IAssets>();
            _gameFactory = diContainer.Resolve<IGameFactory>();
            _sceneLoader = diContainer.Resolve<ISceneLoader>();
            _curtain = diContainer.Resolve<ILoadingCurtain>();
            _stageService = diContainer.Resolve<IStageObserverService>();
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();

            LoadConfig();

            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        private void LoadConfig() =>
            _config = _assetProvider.Load<LevelConfig>(AssetPath.Config);

        private void OnLoaded()
        {
            InitGameWorld();



            _stateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            var stages = _gameFactory.CreateLevel(_config);

            _stageService.RegisterStages(stages);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
    }
}
