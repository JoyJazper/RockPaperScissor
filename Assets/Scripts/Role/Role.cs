using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using RPS.Enums;
using RPS;

[Serializable]
[CreateAssetMenu(fileName = "Role", menuName = "GameRule/CreateRole")]
public class Role : ScriptableObject
{
    [Header("Role Name")]
    public RoleType role;

    [Header("Role Symbol")]
    public Sprite roleSymbol;

    [Header("Role Rules")]
    public ActionMap[] actionMap;
}


namespace RPS
{
    [Serializable]
    public struct ActionMap{
        public RoleType key;
        public bool canInfluence;
        public actions actionType;
    }
}


