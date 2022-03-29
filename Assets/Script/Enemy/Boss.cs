using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyStatusBase
{
    [SerializeField]
    private GameObject bossHpbarObj;
    [SerializeField]
    private Slider bossHpbar;

    protected override void Start()
    {
        base.Start();
        bossHpbar.value = 1.0f;
    }
    protected override void Update()
    {
        base.Update();
        if (transform.position.y < -10)
        {
            gameObject.transform.position = new Vector3(0f, 15f, 80f);
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void AttackStart()
    {
        base.AttackStart();
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
    }
    public override float PlayerAttackDamageAmount(float attackPower, float defense)
    {
        return base.PlayerAttackDamageAmount(attackPower, defense);
    }
    /// <summary>
    /// 敵のHPの現在値とHpbarのvalueを同期させる
    /// </summary>

    public void UpdatedBossHpBarValue(float hp, float maxHp)
    {
        bossHpbar.value = (float)hp / (float)maxHp;
    }
    /// <summary>
    /// 敵が倒された時にHpBarを隠す
    /// </summary>
    public void HideHpBar()
    {
        bossHpbarObj.SetActive(false);
    }


    private IEnumerator BossDie()
    {
        if (HP <= 0)
        {
            HideHpBar();
            animator.SetTrigger("Death");

            yield return new WaitForSeconds(1);
            Destroy(gameObject, 1f);
        }
    }

    //Triggerの中で攻撃処理実行 //弾の接触
    //通常弾の設定
    /// <summary>
    ///通常弾、貫通弾は雑魚敵と同じダメージ
    ///毒弾・即死弾はダメージのみ（特殊効果なし）
    /// </summary>
    /// <param name="other"></param>
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        StartCoroutine(BossDie());
        //毒弾接触検知
        if (other.gameObject.CompareTag("PoisonBullet"))
        {
            Destroy(other.gameObject);

            PoisonBulletGetHitDamage();

            StartCoroutine(BossDie());

            animator.SetTrigger("GetHit");
        }

        //即死弾接触検知
        if (other.gameObject.CompareTag("DeathBullet"))
        {
            Destroy(other.gameObject);

            DeathBulletGetHitDamage();

            StartCoroutine(BossDie());

            animator.SetTrigger("GetHit");
        }
    }

    protected override void BulletGetHitDamage()
    {
        base.BulletGetHitDamage();
        UpdatedBossHpBarValue(hp, maxHp);
    }

    //貫通弾ダメージ
    //敵のダメージ処理
    protected override void PenetrationBulletGetHitDamage()
    {
        base.PenetrationBulletGetHitDamage();
        UpdatedBossHpBarValue(hp, maxHp);
    }

    //毒弾のダメージ
    protected override void PoisonBulletGetHitDamage()
    {
        base.PoisonBulletGetHitDamage();
        UpdatedBossHpBarValue(hp, maxHp);
    }

    //即死弾のダメージ
    protected override void DeathBulletGetHitDamage()
    {
        base.DeathBulletGetHitDamage();
        UpdatedBossHpBarValue(hp, maxHp);
    }
}