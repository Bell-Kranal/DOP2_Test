using Data;
using Infrastructure.AssetManagement;
using Logic;
using Logic.TouchErase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly LevelModel _levelModel;
        private readonly DiContainer _diContainer;

        private SoapAnimation _soapAnimation;
        private LevelView _levelView;
        private bool _uiIsCreated;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, LevelModel levelModel, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _levelModel = levelModel;
            _diContainer = diContainer;
            _uiIsCreated = false;
        }

        public void CreateUI()
        {
            if (_uiIsCreated)
                return;
            
            GameObject ui = _assetProvider.LoadUI();
            GameObject gameObject = Object.Instantiate(ui);

            _levelView = gameObject.GetComponentInChildren<LevelView>();
            _soapAnimation = gameObject.GetComponentInChildren<SoapAnimation>();
            _levelModel.NextLevelButton = gameObject.GetComponentInChildren<NextLevelButton>().GetComponent<Button>();
            _levelModel.WinLevelIcon = gameObject.GetComponentInChildren<WinLevelIcon>();
            
            _soapAnimation.gameObject.SetActive(false);
            _levelModel.NextLevelButton.gameObject.SetActive(false);
            _levelModel.WinLevelIcon.gameObject.SetActive(false);

            _uiIsCreated = true;
        }

        public void CreateLevel(int level)
        {
            LevelData levelData = _staticDataService.ForLevelData(level);

            GameObject gameObject = _diContainer.InstantiatePrefab(levelData.Level, Vector3.zero, Quaternion.identity, null);
            TouchErase[] touchErases = gameObject.GetComponentsInChildren<TouchErase>();

            foreach (TouchErase touch in touchErases)
            {
                touch.SetSoapTransform(_soapAnimation);
            }
            
            _levelView.SetValues(level, levelData.Description);
            _levelModel.Level = gameObject;
        }
    }
}