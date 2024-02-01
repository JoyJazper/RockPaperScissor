using System.Collections.Generic;

internal class UIStateManager : IUIStateManager
{
    private Dictionary<UIStates, IUIState> states = new Dictionary<UIStates, IUIState>();
    public UIStates currentState = UIStates.Menu;
    public void Init()
    {
        
    }

    public void ChangeUIState(UIStates toState)
    {
        currentState = toState;
        ExitAllStates();
        if (states.ContainsKey(toState))
            states[toState].OnStateEnter();
    }

    public void ExitAllStates()
    {
        if (states != null)
        {
            foreach (var state in states.Values)
            {
                state.OnStateExit();
            }
        }
    }

    public void Start()
    {
        IUIState Menu = new UIMenu();
        states.Add(UIStates.Menu, Menu);

        IUIState Deck = new UIDeck();
        states.Add(UIStates.Deck, Deck);

        IUIState Game = new UIGame();
        states.Add(UIStates.Game, Game);

        IUIState End = new UIEnd();
        states.Add(UIStates.End, End);
    }

    public void Destroy()
    {
        states?.Clear();
        states = null;
    }
}

