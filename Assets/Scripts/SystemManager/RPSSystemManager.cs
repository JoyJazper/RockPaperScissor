using RPS.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPS.Systems
{
    public class RPSSystemManager : Singleton<RPSSystemManager>
    {
        internal GameManager game;
        internal UIManager uiManager;
        internal UIStateManager uiStateManager;
        //internal LevelProgressManager levelStat;

        protected override void Awake()
        {
            base.Awake();
            game = new GameManager();
            //levelStat = new LevelProgressManager();
            uiManager = new UIManager();
            uiStateManager = new UIStateManager();
            
        }

        private void Start()
        {
            game.Init();
            uiManager.Init();
            uiStateManager.Init();
            uiManager.Start();
            game.Start();
            uiStateManager.Start();
        }

        protected override void OnDestroy()
        {
            game?.Destroy();
            uiManager?.Destroy();
            uiStateManager?.Destroy();
            //levelStat.Destroy();

            game = null;
            uiManager = null;
            uiStateManager = null;
            //levelStat = null;
            
            base.OnDestroy();
        }
    }
}

