using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class UIStateManager : IRPSSystem
{
    private Dictionary<UIStates, IUIState> states = new Dictionary<UIStates, IUIState>();
    public UIStates currentState = UIStates.Menu;

    public void Destroy()
    {
        states?.Clear();
        states = null;
    }

    public void Init()
    {
        IUIState Menu = new UIMenu();
        states.Add(UIStates.Menu, Menu);

        IUIState Deck = new UIDeck();
        states.Add(UIStates.Deck, Deck);

        IUIState Game = new UIGame();
        states.Add(UIStates.Game, Game);

        IUIState End = new UIEnd();
        states.Add(UIStates.End, End);
        ChangeUIState(UIStates.Menu);
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
        if(states != null)
        {
            foreach (var state in states.Values)
            {
                state.OnStateExit();
            }
        }
    }

    public void Start()
    {
        
    }
}

