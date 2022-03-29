using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGate : MonoBehaviour
{
    [SerializeField]
    private GameObject monsterPrefab;

    //コルーチンを使って増殖
    void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        //ずっと繰り返し処理　for文で回数の指定も可能
        while (true)
        {
            //出現位置
            Instantiate(monsterPrefab, new Vector3(Random.Range(0f, 10f), 0, Random.Range(0f, 20f)), Quaternion.identity);

            //　new　忘れない事　（）の中は秒数
            yield return new WaitForSeconds(8);

            //秒数調べるのにTime.timeは便利
           // Debug.Log(Time.time);
        }
    }
}