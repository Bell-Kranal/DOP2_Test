using UnityEngine;

namespace Data
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelDataPath = "StaticData/Level/LevelData";
        
        private LevelStaticData _levelsData;

        private bool _isLoaded;

        public StaticDataService() =>
            _isLoaded = false;

        public bool IsLoaded => _isLoaded;

        public void Load()
        {
            ResourceRequest loadOperation = Resources.LoadAsync(LevelDataPath);

            loadOperation.completed += _ =>
            {
                _levelsData = loadOperation.asset as LevelStaticData;
                _isLoaded = true;
            };
        }

        public LevelData ForLevelData(int levelIndex) =>
            levelIndex >= _levelsData.LevelPrefabs.Count ? _levelsData.LevelPrefabs[0] : _levelsData.LevelPrefabs[levelIndex];
    }
}