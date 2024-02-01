using System;
using RPS.Constants;
using RPS.Models;
using RPS.Systems;

public class UIManager : IUIManager
{
    public delegate void PlayerCardDetail(PlayerCard role);
    public static event PlayerCardDetail PlayerHandSelected;

    private UIReferences uiReferences;
    private IUIStateManager uiStateManager;

    public void Init() { }
    public void Start()
    {
        uiReferences = UIReferences.Instance;
        uiStateManager = RPSSystemManager.Instance.uiStateManager;

        UIMenu.OnPlayButton += PlayDeck;
        UIDeck.OnDeckButton += PlayGame;
        UIEnd.OnContinueClick += ContinueClicked;

        GameUtility.Instance.DelayOneFrame(PlayMenu);
    }
    
    private void ContinueClicked()
    {
        RPSSystemManager.Instance.game.IncreaseLevel();
    }

    public void PlayMenu()
    {
        uiStateManager.ChangeUIState(UIStates.Menu);
        uiReferences.playLevelText.text = GameConstants.PLAY_LEVEL + GameData.level.Value.ToString();
    }
    public void PlayDeck()
    {
        uiStateManager.ChangeUIState(UIStates.Deck);
        AudioManager.Instance.PlayBGMFX(AudioClipID.DeckBG);
        AudioManager.Instance.PlaySFX(AudioClipID.PlaySelect);
        
        EnableInstruction(GameConstants.GET_CARD);
        RPSSystemManager.Instance.game.SetupDeck();
    }
    public void PlayGame()
    {
        uiStateManager.ChangeUIState(UIStates.Game);
        RPSSystemManager.Instance.game.StartGame();
    }
    public void GameEnded()
    {
        uiStateManager.ChangeUIState(UIStates.End);
        DisableInstruction();
    }

    public void EnableCountdown(float targetTime)
    {
        uiReferences.CountdownBase.gameObject.SetActive(true);
        uiReferences.CountdownBase.Init(targetTime, DisableCountdown);
    }
    public void DisableCountdown()
    {
        uiReferences.CountdownBase.gameObject.SetActive(false);
    }

    public void EnableInstruction(string instruction)
    {
        var instructionText = uiReferences.instructionText;
        var instructionBase = uiReferences.instructionBase;

        instructionText.gameObject.SetActive(true);
        instructionBase.gameObject.SetActive(true);
        instructionText.text = instruction;
    }
    public void DisableInstruction()
    {
        var instructionText = uiReferences.instructionText;
        var instructionBase = uiReferences.instructionBase;

        instructionText.gameObject.SetActive(false);
        instructionBase.gameObject.SetActive(false);
        instructionText.text = "";
    }

    public void ShowPlayerVictory(string action)
    {
        uiReferences.BG_Win.DoPunch();

        uiReferences.blast.gameObject.SetActive(true);
        uiReferences.blast.PlayAnimation(BlastsAnimations.Blast2);

        EnableInstruction(string.Format(GameConstants.WINNER, action));
        
        ShowHands();
    }
    public void ShowEnemyVictory(string action)
    {
        uiReferences.BG_Lose.DoPunch();

        uiReferences.blast.gameObject.SetActive(true);
        uiReferences.blast.PlayAnimation(BlastsAnimations.Blast3);

        EnableInstruction(string.Format(GameConstants.LOSER, action));

        ShowHands();
    }
    public void ShowNormalBG()
    {
        uiReferences.BG_Win.FadeOut();
        uiReferences.BG_Lose.FadeOut();

        uiReferences.blast.gameObject.SetActive(false);

        EnableInstruction(GameConstants.NEUTRAL);

        ShowHands();
    }

    public void ShowPlayerHand(PlayerCard card)
    {
        AudioManager.Instance.PlaySFX(AudioClipID.CardSelect);

        var playerHand = uiReferences.playerhand;
        playerHand.sprite = GameData.GetPlayerSprite(card.Role);
        playerHand.gameObject.SetActive(true);

        PlayerHandSelected?.Invoke(card);
    }

    public void ShowHands()
    {
        GameData.lockPlayerInput = true;
        uiReferences.playerhand.gameObject.SetActive(true);
        uiReferences.enemyhand.gameObject.SetActive(true);
    }
    public void HideHands()
    {
        GameData.lockPlayerInput = false;
        uiReferences.playerhand.gameObject.SetActive(false);
        uiReferences.enemyhand.gameObject.SetActive(false);
    }


    public void Destroy()
    {
        UIMenu.OnPlayButton -= PlayDeck;
        UIDeck.OnDeckButton -= PlayGame;
        UIEnd.OnContinueClick -= ContinueClicked;
    }
}

