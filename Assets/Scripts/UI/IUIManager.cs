using RPS.Enums;

public interface IUIManager : IRPSSystem
{
    void EnableCountdown(float targetTime);
    void DisableCountdown();
    
    void EnableInstruction(string instruction);
    void DisableInstruction();
    
    void ShowPlayerVictory(string action);
    void ShowEnemyVictory(string action);
    void ShowNormalBG();
    
    void ShowPlayerHand(RoleType card);
    
    void ShowHands();
    void HideHands();
    void PlayDeck();
    void GameEnded();
}
