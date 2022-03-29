using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescue : MonoBehaviour
{
    [Header("全快カウント")]
    [SerializeField]
    private int liefCount;

    [Header("全快限界")]
    private int maxLiefCount = 2;

    private PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }


    void Update()
    {
        Rescuelife();
    }

    //瀕死状態から全回復　回数制限あり
    public void Rescuelife()
    {
        if (playerStatus.HP <= 50f)
        {
            liefCount++;

            if (liefCount <= maxLiefCount)
            {
                playerStatus.PlayerRescue(playerStatus.HP);
                Debug.Log("全快" + playerStatus.MaxHp);

                playerStatus.HP = playerStatus.MaxHp;

                playerStatus.UpdatedHpBarValue(playerStatus.HP, playerStatus.MaxHp);
            }
        }
    }
}