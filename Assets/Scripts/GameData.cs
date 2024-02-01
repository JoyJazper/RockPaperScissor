using RPS.Enums;
using RPS;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using RPS.Systems;

namespace RPS.Models
{
    public class GameData : IRPSModel
    {
        internal static ObservableVariable<float> currentProgress;
        internal static ObservableVariable<int> level;

        internal static bool lockPlayerInput = false;
        internal static bool isLastLevel = false;
        internal static LevelData currentLevelData = null;

        internal static Dictionary<RoleType, Dictionary<RoleType, ActionMap>> rolesInGameMap = new Dictionary<RoleType, Dictionary<RoleType, ActionMap>>();
        internal static Dictionary<RoleType, Sprite> roleSprites = new Dictionary<RoleType, Sprite>();

        internal static event Action OnGameDataDestroy;

        public static List<RoleType> GetRandomRoles(int n)
        {
            List<RoleType> roles = new List<RoleType>(rolesInGameMap.Keys);
            GameUtility.ShuffleList(roles);
            return roles.Take(n).ToList();
        }

        public static Sprite GetPlayerSprite(RoleType role)
        {
            return roleSprites.TryGetValue(role, out Sprite sprite) ? sprite : null;
        }

        public static ActionMap GetAction(RoleType playerRole, RoleType enemyRole)
        {
            if (!rolesInGameMap.TryGetValue(playerRole, out Dictionary<RoleType, ActionMap> actionMap))
            {
                return new ActionMap { key = RoleType.None };
            }

            return actionMap.TryGetValue(enemyRole, out ActionMap action) ? action : new ActionMap { key = RoleType.None };
        }

        internal void ClearData()
        {
            if (rolesInGameMap != null)
                foreach (var roleActionMap in rolesInGameMap.Values)
                {
                    roleActionMap?.Clear();
                }
            rolesInGameMap?.Clear();
            roleSprites?.Clear();
        }

        public void Init()
        {
            currentProgress = new ObservableVariable<float>(0f);
            level = new ObservableVariable<int>(0);
        }

        public void Destroy()
        {
            OnGameDataDestroy?.Invoke();
            ClearData();
        }
    }

}



