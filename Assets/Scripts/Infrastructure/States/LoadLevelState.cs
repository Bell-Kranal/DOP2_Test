using Data;
using Infrastructure.Factories;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistantProgressService _progressService;

        public LoadLevelState(IGameFactory gameFactory, IPersistantProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter()
        {
            _gameFactory.CreateUI();
            _gameFactory.CreateLevel(_progressService.GetLevel());
        }

        public void Exit() { }
    }
}