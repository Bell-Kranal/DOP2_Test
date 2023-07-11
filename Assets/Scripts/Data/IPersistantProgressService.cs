namespace Data
{
    public interface IPersistantProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
        public int GetLevel();
        public void IncrementLevel();
    }
}