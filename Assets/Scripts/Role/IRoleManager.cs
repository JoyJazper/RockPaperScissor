namespace RPS.Game
{
    internal interface IRoleManager
    {
        void Destroy();
        void EnableAllCards();
        bool HasCard();
        void LockPlayerRole();
        ActionMap PlayHands();
        bool SelectEnemyRole();
        void SelectPlayerRole(PlayerCard playerRole);
        PlayerCard SelectRandomPlayerCard();
        void SetupEnemyCards();
        void SetupPlayerCards();
        void ShowHands();
    }
}