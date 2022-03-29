using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWall : MonoBehaviour
{
    [Header("攻撃回数")]
    private int destroyCount =5;

    [Header("耐久値")]
    [SerializeField]
    private int destroy = 0;

    /// <summary>
    /// 通常弾のみ適応
    /// 耐久値が0になると破壊される
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            destroyCount--;

            if (destroyCount <= destroy)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}