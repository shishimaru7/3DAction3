using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///銃弾が攻撃力を持つ
///敵のHP＝銃弾の威力-敵の防御力
/// </summary>
public class Bullet : BulletBase
{
    [SerializeField]
    protected float bulletPower;
    public float getBulletPower { get { return bulletPower; } }

    protected override void Start()
    {
        base.Start();
    }
    //　通常弾 敵にダメージの処理　銃弾の攻撃力-敵の防御力
    public float BulletAttackDamageAmount(float bulletAttackPower, float defense)
    {
        float damage = Mathf.Clamp(bulletAttackPower - defense, 0, bulletAttackPower);
        Debug.Log("バレットのダメージ量" + damage);
        return damage;
    }
}