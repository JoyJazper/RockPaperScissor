using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using RPS;
using RPS.Enums;
using RPS.Constants;
using RPS.Game;

public class GameUtility : Singleton<GameUtility>
{
    private List<Role> rolesInGame;
    public void SetRolesInGame(List<Role> roles, UnityAction OnUpdate = null)
    {
        rolesInGame = roles;
        StartCoroutine(CreateActionMap(OnUpdate));
    }
    private Dictionary<RoleType, Dictionary<RoleType, ActionMap>> rolesInGameMap = new Dictionary<RoleType, Dictionary<RoleType, ActionMap>>();
    private Dictionary<RoleType, Sprite> roleSprites = new Dictionary<RoleType, Sprite>();
    protected override void Awake()
    {
        base.Awake();
        
    }

    private IEnumerator CreateActionMap(UnityAction OnUpdate = null)
    {
        foreach (Role role in rolesInGame)
        {
            roleSprites.Add(role.role, role.roleSymbol);
            Dictionary<RoleType, ActionMap> actionTrueMap = new Dictionary<RoleType, ActionMap>();
            rolesInGameMap.Add(role.role, actionTrueMap);
        }
        foreach (Role role in rolesInGame)
        {
            foreach (ActionMap map in role.actionMap)
            {
                (rolesInGameMap[role.role]).Add(map.key, map);
            }
        }
        yield return null;
        if(OnUpdate != null)
        {
            OnUpdate();
        }
    }

    

    public List<RoleType> GetnRoles(int n)
    {
        List<RoleType> roles = new List<RoleType> ();
        List<RoleType> rolesTemp = new List<RoleType> (rolesInGameMap.Keys);
        int temp;
        for (int x = 0; x < n; x++) 
        {
            temp = GetRoleNumber(rolesTemp.Count-1);
            roles.Add(rolesTemp[temp]);
            rolesTemp.RemoveAt(temp);
        }
        rolesTemp.Clear ();
        rolesTemp = null;
        return roles;
    }

    private int GetRoleNumber(int count)
    {
        return UnityEngine.Random.Range(0, count);
    }

    public Sprite GetPlayerSprite(RoleType role) 
    {
        return roleSprites[role];
    }

    public ActionMap GetAction(RoleType playerRole, RoleType enemyRole)
    {
        if(instance.rolesInGameMap == null || playerRole == RoleType.None || enemyRole == RoleType.None)
        {
            //Debug.LogError("ERNOS : returning null " + playerRole.ToString() + " " + enemyRole.ToString());
            ActionMap actionMap = new ActionMap();
            actionMap.key = RoleType.None;
            return actionMap;
        }
        //Debug.LogError("ERNOS : Game is : " + playerRole.ToString() + " and " + enemyRole.ToString());
        return (instance.rolesInGameMap[playerRole])[enemyRole];
    }



    public void StartTimer(float time, UnityAction timerStopEvent)
    {
        StartCoroutine(timer(time, timerStopEvent));
    }

    public IEnumerator timer(float time, UnityAction timerStopEvent)
    {
        //Debug.LogError("Starting timer");
        yield return new WaitForSeconds(time);
        timerStopEvent();
    }

    public static RoleType SelectRandomRole(List<RoleType> roleList)
    {
        int x = UnityEngine.Random.Range(0, roleList.Count);
        return roleList[x];
    }

    public static float RemapValue(float value, float minValue, float maxValue, float min, float max)
    {
        return (value - minValue) / (maxValue - minValue) * (max - min) + min;
    }

    protected override void OnDestroy()
    {
        foreach (Role role in rolesInGame)
        {
            rolesInGameMap[role.role].Clear();
        }
        roleSprites.Clear();
        roleSprites = null;
        rolesInGameMap.Clear();
        rolesInGameMap = null;
        StopAllCoroutines();
        base.OnDestroy();
    }
}
