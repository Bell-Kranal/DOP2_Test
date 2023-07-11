using Data;
using UnityEngine;

namespace Infrastructure.States
{
    public class WinLevelState : IState
    {
        private readonly LevelModel _levelModel;
        private readonly IPersistantProgressService _progressService;
        private readonly IGameStateMachine _gameStateMachine;

        public WinLevelState(LevelModel levelModel, IPersistantProgressService progressService, IGameStateMachine gameStateMachine)
        {
            _levelModel = levelModel;
            _progressService = progressService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _levelModel.Level.transform.GetChild(0).gameObject.SetActive(false);
            _levelModel.Level.transform.GetChild(1).gameObject.SetActive(true);
            _levelModel.NextLevelButton.gameObject.SetActive(true);
            _levelModel.WinLevelIcon.gameObject.SetActive(true);
            _progressService.IncrementLevel();
            
            _levelModel.NextLevelButton.onClick.AddListener(() =>
            {
                _levelModel.NextLevelButton.gameObject.SetActive(false);
                _levelModel.WinLevelIcon.gameObject.SetActive(false);
                _gameStateMachine.Enter<LoadLevelState>();
            });
        }

        public void Exit()
        {
            Object.Destroy(_levelModel.Level);
            _levelModel.NextLevelButton.onClick.RemoveAllListeners();
        }
    }
}