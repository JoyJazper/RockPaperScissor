using RPS.Game;
using RPS.Models;

namespace RPS.Systems
{
    public class RPSSystemManager : Singleton<RPSSystemManager>
    {
        internal IGameManager game;
        internal IUIManager uiManager;
        internal IUIStateManager uiStateManager;
        internal ILevelManager levelManager;
        //internal LevelProgressManager levelStat;

        internal IRPSModel gameData;

        protected override void Awake()
        {
            gameData = new GameData();
            gameData.Init();

            game = new GameManager();
            uiManager = new UIManager();
            uiStateManager = new UIStateManager();
            levelManager = new LevelManager();

            game.Init();
            uiManager.Init();
            uiStateManager.Init();
            levelManager.Init();
            base.Awake();
        }

        private void Start()
        {
            uiManager.Start();
            game.Start();
            uiStateManager.Start();
            levelManager.Start();
        }

        protected override void OnDestroy()
        {
            game?.Destroy();
            uiManager?.Destroy();
            uiStateManager?.Destroy();
            levelManager?.Destroy();
            //levelStat.Destroy();

            game = null;
            uiManager = null;
            uiStateManager = null;
            levelManager = null;
            //levelStat = null;
            gameData?.Destroy();
            base.OnDestroy();
        }
    }
}

