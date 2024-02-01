namespace RPS.Game
{
    public interface IGameManager : IRPSSystem
    {
        void IncreaseLevel();
        void SetupDeck();
        void StartGame();
    }
}