using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Logic.TouchErase
{
    public sealed class TouchWinErase : TouchErase
    {
        [SerializeField] private int _needUsedCountCoordinates;
        
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        protected override void CheckWin()
        {
            if (UsedTextureCoordinates.Count < _needUsedCountCoordinates)
            {
                ResetRenderTexture();
                UsedTextureCoordinates.Clear();
            }
            else
            {
                _gameStateMachine.Enter<WinLevelState>();
            }
        }
    }
}