using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationBullet : BulletBase
{
    [SerializeField]
    private float penetrationBulletPower;
    public float getPenetrationBulletPower { get { return penetrationBulletPower; } }

    protected override void Start()
    {
        base.Start();
    }
    //貫通弾　　敵にダメージの処理　銃弾の攻撃力-敵の防御力
    public float PenetrationBulletAttackDamageAmount(float bulletAttackPower, float defense)
    {
        float damage = Mathf.Clamp(bulletAttackPower - defense, 0, bulletAttackPower);
        Debug.Log("貫通弾のダメージ量" + damage);
        return damage;
    }
}