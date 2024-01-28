using RPS.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPS.Systems
{
    public class RPSSystemManager : Singleton<RPSSystemManager>
    {
        internal GameManager game;
        //internal LevelProgressManager levelStat;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            game = new GameManager();
            //levelStat = new LevelProgressManager();

            game.Init();
            //levelStat.Init();
        }

        protected override void OnDestroy()
        {
            game.Destroy();
            //levelStat.Destroy();

            game = null;
            //levelStat = null;
            
            base.OnDestroy();
        }
    }
}

