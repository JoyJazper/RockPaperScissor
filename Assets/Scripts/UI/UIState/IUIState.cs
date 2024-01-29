public interface IUIState 
{
    public void OnStateEnter();

    public void OnStateExit();
}

public enum UIStates
{
    Menu = 0,
    Deck = 1,
    Game = 2,
    End = 3,
    none
}