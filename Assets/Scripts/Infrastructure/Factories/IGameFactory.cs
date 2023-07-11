namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        public void CreateUI();
        public void CreateLevel(int level);
    }
}