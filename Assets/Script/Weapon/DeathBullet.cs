using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBullet : BulletBase
{
    [SerializeField]
    private float deathBulletPower;
    public float getDeathBulletPower { get { return deathBulletPower; } }

    protected override void Start()
    {
        base.Start();
    }
    //　即死弾　敵にダメージの処理　銃弾の攻撃力-敵の防御力
    public float DeathBulletAttackDamageAmount(float bulletAttackPower, float defense)
    {
        float damage = Mathf.Clamp(bulletAttackPower - defense, 0, bulletAttackPower);
        Debug.Log("即死弾のダメージ量" + damage);
        return damage;
    }
}