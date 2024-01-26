using RPS.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPS.Systems
{
    public class RPSSystemManager : Singleton<RPSSystemManager>
    {
        private GameManager game;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            game = new GameManager();
            game.Init();
        }

        protected override void OnDestroy()
        {
            game.Destroy();
            base.OnDestroy();
        }
    }
}

