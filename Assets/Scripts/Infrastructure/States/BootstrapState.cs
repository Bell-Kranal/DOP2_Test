using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Zenject;

namespace Infrastructure.States
{
    public class BootstrapState : IPayloadState<string>
    {
        private readonly DiContainer _diContainer;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly LevelModel _levelModel;

        public BootstrapState(DiContainer diContainer, SceneLoader sceneLoader, IGameStateMachine gameStateMachine, LevelModel levelModel)
        {
            _diContainer = diContainer;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
            _levelModel = levelModel;

            RegisterServices();
        }

        public async void Enter(string sceneName)
        {
            await _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() { }

        private void RegisterServices()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();

            _sceneLoader.DataIsLoaded += () => staticDataService.IsLoaded;

            IGameFactory gameFactory = new GameFactory(new AssetProvider(), staticDataService, _levelModel, _diContainer);
            IPersistantProgressService progressService = new PersistantProgressService();
            progressService.PlayerProgress = new PlayerProgress();

            _diContainer.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
            _diContainer.Bind<IGameFactory>().FromInstance(gameFactory).AsSingle();
            _diContainer.Bind<IPersistantProgressService>().FromInstance(progressService).AsSingle();
        }

        private void OnLoaded() =>
            _gameStateMachine.Enter<LoadLevelState>();
    }
}