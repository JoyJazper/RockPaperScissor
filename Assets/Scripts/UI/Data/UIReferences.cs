using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIReferences : Singleton<UIReferences>
{
    [SerializeField] internal Button play;
    [SerializeField] internal Button deck;
    [SerializeField] internal Button levelUp;
    [SerializeField] internal List<PlayerCard> playerCards;
    [SerializeField] internal List<EnemyCard> enemyCards;
 
    [SerializeField] internal Transform playBase;
    [SerializeField] internal Transform deckBase;
    [SerializeField] internal Transform playerCardBase;
    [SerializeField] internal Transform enemyCardBase;
    [SerializeField] internal Transform BoardBase;
    [SerializeField] internal Transform levelUpBase;

    [SerializeField] internal TimerAnimation CountdownBase;
    [SerializeField] internal TMP_Text instructionText;
    [SerializeField] internal Transform instructionBase;
    [SerializeField] internal TMP_Text levelUpText;
    [SerializeField] internal TMP_Text playLevelText;
    [SerializeField] internal BlastPlayer blast;

    [SerializeField] internal Image levelProgress;
    [SerializeField] internal BackgroundAnimation BG_Win;
    [SerializeField] internal BackgroundAnimation BG_Lose;

    [SerializeField] internal Image playerhand;
    [SerializeField] internal Image enemyhand;
}
