using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip audioClip;

    private float heal;

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    public void AddHeal(PlayerStatus playerStatus)
    {
        heal = playerStatus.MaxHp;
        playerStatus.HP += heal;

        Debug.Log(heal+"回復");
        audioSource = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}