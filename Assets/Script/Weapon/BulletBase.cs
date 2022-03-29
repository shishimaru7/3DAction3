using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//親はScript付けない　MonoBehaviourは親
//※子にScriptは付ける
//継承すればメソッドはまとめて使える
public class BulletBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject shotEffect;
    [SerializeField]
    protected AudioClip audioClip;

    protected virtual void Start()
    {
        Destroy(gameObject, 3f);
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        GameObject effect = Instantiate(shotEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
}