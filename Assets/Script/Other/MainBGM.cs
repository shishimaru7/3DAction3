using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip bossBGM;
    private bool isBossBGM;
    private AudioSource audioSource;
    private Boss boss;

    void Start()
    {
        isBossBGM = true;
        audioSource = GetComponent<AudioSource>();

        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }
    private void Update()
    {
        ChangeBGM();
    }
    public void ChangeBGM()
    {
        if (boss.HP <= 10 && isBossBGM)
        {
            isBossBGM = false;
            audioSource.Stop();

            audioSource.clip = bossBGM;

            audioSource.Play();
            Debug.Log("bossBGM再生");
        }
    }
}