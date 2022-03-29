using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の基底クラス
/// </summary>
public class EnemyStatusBase : CharacterBase
{
    [Header("敵の攻撃力")]
    [SerializeField]
    protected float enemyAttackPower;
    public float EnemyAttackPower { set { enemyAttackPower = value; } get { return enemyAttackPower; } }
    [Header("間合い")]
    [SerializeField]
    protected float distance;

    //敵が受けるヒット
    [Header("ヒット限界数")]
    protected int hitmax = 3;

    [Header("毒の蓄積値")]
    protected float poisoncharge;

    [Header("毒の蓄積値最大")]
    protected float poisonchargeMAX = 100f;

    [Header("敵毒の状態時間")]
    protected float poisonTime;

    [Header("敵毒の最大状態時間")]
    protected float poisonTimeMAX = 5f;
    protected BoxCollider boxCollider;

    protected GameObject target;

    protected Bullet bullet;

    protected PenetrationBullet penetrationBullet;

    protected PoisonBullet poisonBullet;

    protected DeathBullet deathBullet;

    protected PlayerStatus playerStatus;

    protected MonsterManager monsterManager;
    protected virtual void Start()
    {
        playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        boxCollider = GameObject.Find("EnemyWeapon").GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    protected virtual void Update()
    {
        Poison();

        RecoveryPoison();
    }
    protected virtual void FixedUpdate()
    {
        AttackStart();
        EnemyMove();
    }

    protected virtual void AttackStart()
    {///////////////////止まっているときに攻撃
        if (dir == Vector3.zero)
        {
            animator.SetBool("Attack", true);
        }
    }

    protected virtual void EnemyMove()
    {
        //自身から見て前方に進む
        rb.AddForce(transform.forward * moveSpeed);
        target = GameObject.Find("Player");
        transform.LookAt(target.transform);

        if (Vector3.Distance(transform.position, target.transform.position) >= distance)
        {
            //基準になる数値0.1,0.2で動く
            animator.SetFloat("Move", 0.3f);
        }
        else
        {
            //基準になるLess数値0.1より上で止まる
            animator.SetFloat("Move", 0f);
        }
    }

    //Monster→Player攻撃処理
    public virtual float PlayerAttackDamageAmount(float attackPower, float defense)
    {
        float damage = Mathf.Clamp( attackPower - defense,0,attackPower);

        return damage;
    }

    //毒の蓄積処理 　弾接触後追加接触限界値超える
    protected virtual void Poisoncharge(float poison)
    {
        {
            poisoncharge += 5f;
        }
    }
    //時間でHP減らすことが出来る
    //毒の開始と毒の終わりが分かれば使える
    protected virtual void Poison()
    {
        //毒の開始蓄積超えると発動
        if (poisoncharge >= poisonchargeMAX)
        {
            //この書き方なら条件で時間の制御は可能　if文の条件は重要
            poisonTime += Time.deltaTime;

            if (poisonTime >= poisonTimeMAX)
            {
                poisonTime = 0;

                //減らす量
                hp -= 5;
            }
        }
    }

    //解毒処理　毒蓄積が減ると毒が止まる
    protected virtual void RecoveryPoison()
    {
        if (poisoncharge >= poisonchargeMAX)
        {
            poisoncharge -= Time.deltaTime;
        }
    }

    //Triggerの中で攻撃処理実行 //弾の接触
    //通常弾の設定
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            BulletGetHitDamage();

            animator.SetTrigger("GetHit");
        }

        //貫通弾の接触設定（ヒット数後で決める）時間による消滅の処理も書く
        if (other.gameObject.CompareTag("PenetrationBullet"))
        {
            penetrationBullet = GameObject.FindGameObjectWithTag("PenetrationBullet").GetComponent<PenetrationBullet>();

            animator.SetTrigger("GetHit");

            //１回当たって多段ヒットさせたい場合
            for (int hit = 0; hit < hitmax; hit++)
            {
                Debug.Log(hitmax + "hit");

                PenetrationBulletGetHitDamage();
            }
        }

        //毒弾接触検知
        if (other.gameObject.CompareTag("PoisonBullet"))
        {
            Destroy(other.gameObject);

            PoisonBulletGetHitDamage();

            Poisoncharge(poisoncharge);

            animator.SetTrigger("GetHit");
        }

        //即死弾接触検知
        if (other.gameObject.CompareTag("DeathBullet"))
        {
            Destroy(other.gameObject);

            DeathBulletGetHitDamage();

            animator.SetTrigger("GetHit");

            if (Random.Range(0, 100) >= 80)
            {
                Destroy(gameObject);
            }
        }
    }

    //通常弾ダメージ
    //敵のダメージ処理
    protected virtual void BulletGetHitDamage()
    {
        bullet = GameObject.FindGameObjectWithTag("Bullet").GetComponent<Bullet>();

        float rest;
        rest = bullet.BulletAttackDamageAmount(bullet.getBulletPower, defensePower);
        hp -= rest;
    }
    //貫通弾ダメージ
    //敵のダメージ処理
    protected virtual void PenetrationBulletGetHitDamage()
    {
        penetrationBullet = GameObject.FindGameObjectWithTag("PenetrationBullet").GetComponent<PenetrationBullet>();

        float rest;
        rest = penetrationBullet.PenetrationBulletAttackDamageAmount(penetrationBullet.getPenetrationBulletPower, defensePower);
        hp -= rest;
    }

    //毒弾のダメージ
    protected virtual void PoisonBulletGetHitDamage()
    {
        poisonBullet = GameObject.FindGameObjectWithTag("PoisonBullet").GetComponent<PoisonBullet>();

        float rest;
        rest = poisonBullet.PoisonBulletAttackDamageAmount(poisonBullet.getPoisonBulletPower, defensePower);
        hp -= rest;
    }
    //即死弾のダメージ
    protected virtual void DeathBulletGetHitDamage()
    {
        deathBullet = GameObject.FindGameObjectWithTag("DeathBullet").GetComponent<DeathBullet>();

        float rest;
        rest = deathBullet.DeathBulletAttackDamageAmount(deathBullet.getDeathBulletPower, defensePower);
        hp -= rest;
    }
}