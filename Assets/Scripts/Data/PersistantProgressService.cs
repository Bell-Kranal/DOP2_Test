namespace Data
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
        
        public int GetLevel() =>
            PlayerProgress.Level;

        public void IncrementLevel() =>
            PlayerProgress.Level++;
    }
}