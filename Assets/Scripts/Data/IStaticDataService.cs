namespace Data
{
    public interface IStaticDataService
    {
        public bool IsLoaded { get; }
        public void Load();
        public LevelData ForLevelData(int levelIndex);
    }
}