using RPS.Enums;
using RPS;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameData
{
    internal static bool isLastLevel = false;
    internal static float currentProgress = 0f;
    internal static int currentLevel = 0;
    internal static LevelData currentLevelData = null;

    internal static Dictionary<RoleType, Dictionary<RoleType, ActionMap>> rolesInGameMap = new Dictionary<RoleType, Dictionary<RoleType, ActionMap>>();
    internal static Dictionary<RoleType, Sprite> roleSprites = new Dictionary<RoleType, Sprite>();

    public static List<RoleType> GetRandomRoles(int n)
    {
        List<RoleType> roles = new List<RoleType>(rolesInGameMap.Keys);
        ShuffleList(roles);
        return roles.Take(n).ToList();
    }

    private static void ShuffleList<T>(List<T> list)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
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

    internal static void ClearData()
    {
        if(rolesInGameMap != null)
        foreach (var roleActionMap in rolesInGameMap.Values)
        {
            roleActionMap?.Clear();
        }
        rolesInGameMap?.Clear();
        roleSprites?.Clear();
    }
}
