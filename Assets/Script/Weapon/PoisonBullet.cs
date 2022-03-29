using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : BulletBase
{
    [SerializeField]
    private float poisonBulletPower;
    public float getPoisonBulletPower { get { return poisonBulletPower; } }

    protected override void Start()
    {
        base.Start();
    }
    //　毒弾　敵にダメージの処理　銃弾の攻撃力-敵の防御力
    public float PoisonBulletAttackDamageAmount(float bulletAttackPower, float defense)
    {
        float damage = Mathf.Clamp(bulletAttackPower - defense, 0, bulletAttackPower);
        Debug.Log("毒弾のダメージ量" + damage);
        return damage;
    }
}