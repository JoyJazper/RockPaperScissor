using System;
using RPS.Constants;
using RPS.Game;
using RPS.Systems;

public class UIManager : IRPSSystem
{
    public delegate void playerCardDetail(PlayerCard role);
    public static event playerCardDetail playerCardClicked;

    public static event Action ProgressUpdated;

    private UIReferences uIRef;
    private UIStateManager uiStateManager;
    public void Init()
    {
        
    }

    public void Start() 
    {
        uIRef = UIReferences.Instance;
        uiStateManager = RPSSystemManager.Instance.uiStateManager;
        UIMenu.OnPlayButton += PlayDeck;
        UIDeck.OnDeckButton += PlayGame;
        UIEnd.OnContinueClick += RPSSystemManager.Instance.game.GoToNextLevel;
        PlayMenu();
        SetProgression();
    }

    public void PlayMenu() 
    { 
        uiStateManager.ChangeUIState(UIStates.Menu);
        RPSSystemManager.Instance.game.MenuStarted();
        uIRef.playLevelText.text = GameConstants.PLAY_LEVEL + GameData.currentLevel.ToString();
    }
    public void PlayDeck() 
    {
        uiStateManager.ChangeUIState(UIStates.Deck); 
        RPSSystemManager.Instance.game.PlaceDeck();
    }
    public void PlayGame() 
    { 
        uiStateManager.ChangeUIState(UIStates.Game);
        RPSSystemManager.Instance.game.StartGame();
    }
    public void PlayEnd() { uiStateManager.ChangeUIState(UIStates.End); }

   
    public void EnableCountdown(float targetTime)
    {
        uIRef.CountdownBase.gameObject.SetActive(true);
        uIRef.CountdownBase.Init(targetTime, DisableCountdown);
    }
    public void EnableInstruction(string instruction)
    {
        uIRef.instructionText.gameObject.SetActive(true);
        uIRef.instructionBase.gameObject.SetActive(true);
        uIRef.instructionText.text = instruction;
    }
    public void DisableInstruction() 
    {
        uIRef.instructionText.gameObject.SetActive(false);
        uIRef.instructionBase.gameObject.SetActive(false);
        uIRef.instructionText.text = "";
    }
    public void DisableCountdown() 
    {
        uIRef.CountdownBase.gameObject.SetActive(false);
    }

    public void SetProgression()
    {
        ProgressUpdated?.Invoke();
    }

    public void SetPlayerVictory(string action)
    {
        uIRef.blast.PlayAnimation(BlastsAnimations.Blast2);
        EnableInstruction(string.Format(GameConstants.WINNER, action));
        uIRef.BG_Win.DoPunch();
    }

    public void SetEnemyVictory(string action)
    {
        uIRef.blast.PlayAnimation(BlastsAnimations.Blast3);
        EnableInstruction(string.Format(GameConstants.LOSER, action));
        uIRef.BG_Lose.DoPunch();
    }

    public void SetNormalBG() 
    {
        uIRef.BG_Win.FadeOut();
        EnableInstruction(GameConstants.NEUTRAL);
        uIRef.BG_Lose.FadeOut();
    }

    bool lockPlayerInput = false;

    public void SelectPlayerCard(PlayerCard card)
    {
        if(lockPlayerInput) { return; }
        AudioManager.Instance.PlaySFX(AudioClipID.CardSelect);
        uIRef.playerhand.sprite = GameUtility.Instance.GetPlayerSprite(card.Role);
        uIRef.playerhand.gameObject.SetActive(true);
        playerCardClicked.Invoke(card);
    }

    public void HideHands()
    {
        lockPlayerInput = false;
        uIRef.playerhand.gameObject.SetActive(false);
        uIRef.enemyhand.gameObject.SetActive(false);
    }

    public void Destroy() 
    {
        UIMenu.OnPlayButton -= PlayDeck;
        UIDeck.OnDeckButton -= PlayGame;
        UIEnd.OnContinueClick -= RPSSystemManager.Instance.game.GoToNextLevel;
    }
}
