internal interface IUIStateManager : IRPSSystem
{
    void ChangeUIState(UIStates toState);
    void ExitAllStates();
}