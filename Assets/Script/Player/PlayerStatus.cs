using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : CharacterBase
{
    [Header("旋回速度")]
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private GameObject sliderPlayerHpobj;
    [SerializeField]
    private Slider sliderPlayerHp;

    Monster monster;
    Boss boss;

    protected virtual void Start()
    {
        sliderPlayerHp.value = 1.0f;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        monster = GameObject.Find("Monster").GetComponent<Monster>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        GameObject monObj = GameObject.Find("Monster");

        if (monObj != null)
        {
            monster = monObj.GetComponent<Monster>();
        }

        GameObject bossObj = GameObject.Find("boss");

        if (bossObj != null)
        {
            boss = bossObj.GetComponent<Boss>();
        }
    }
    private void Update()
    {
        Debug.Log("Zで攻撃、Xで弾交換、CでCamera切り替え");
        if (transform.position.y < -10)
        {
            gameObject.transform.position = new Vector3(0f, 10f, 0f);
        }
    }
    public void UpdatedHpBarValue(float hp, float maxHp)
    {
        sliderPlayerHp.value = (float)hp / (float)maxHp;
    }
    private void FixedUpdate()
    {
        Stop();
        Move();
    }
    /// <summary>
    /// 敵が倒された時にHpBarを隠す
    /// </summary>
    public void HideHpBar()
    {
        sliderPlayerHpobj.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EnemyWeapon")
        {
            PlayerGetHitDamage(other.gameObject.name);

            StartCoroutine(Death());

            animator.SetTrigger("GetHit");
        }
        if (other.gameObject.name == "BossWeapon")
        {
            PlayerGetHitDamage(other.gameObject.name);

            StartCoroutine(Death());

            animator.SetTrigger("GetHit");
        }

        //回復アイテム取得後
        if (other.gameObject.CompareTag("Heal"))
        {
            other.gameObject.GetComponent<HealItem>().AddHeal(this);
            Destroy(other.gameObject);

            UpdatedHpBarValue(hp, maxHp);
        }
    }

    public void PlayerGetHitDamage(string enemyName)
    {
        switch (enemyName)
        {
            case "EnemyWeapon":
                float damage;
                damage = monster.PlayerAttackDamageAmount(monster.EnemyAttackPower, defensePower);
                HP -= damage;
                Debug.Log(monster + "から" + "Player" + damage + "受ける");
                UpdatedHpBarValue(HP, maxHp);
                break;
            case "BossWeapon":
                damage = boss.PlayerAttackDamageAmount(boss.EnemyAttackPower, defensePower);
                HP -= damage;
                Debug.Log(boss + "から" + "Player" + damage + "受ける");
                UpdatedHpBarValue(HP, maxHp);
                break;
        }
    }

    public void PlayerRescue(float rescue)
    {
        HP += rescue;
        UpdatedHpBarValue(HP, maxHp);
    }

    IEnumerator Death()
    {
        if (HP <= 0)
        {
            Destroy(gameObject, 5f);

            animator.SetTrigger("Death");

            HideHpBar();

            yield return new WaitForSeconds(4);

            SceneManager.LoadScene("GameOver");
        }
    }


    //ボタン離したらすぐその場に止まる
    public void Stop()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (Input.GetButtonUp("Vertical"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    //移動処理
    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        dir = new Vector3(x, 0, z).normalized;
        rb.AddForce(x * moveSpeed, 0, z * moveSpeed);
        LookDirection(dir);

        if (dir != Vector3.zero)
        {
            //基準になる数値0.1,0.2で動く
            animator.SetFloat("Move", 0.3f);
        }
        else
        {//基準になるLess数値0.1より上で止まる
            animator.SetFloat("Move", 0f);
        }
    }

    private void LookDirection(Vector3 dir)
    {
        // ベクトル(向きと大きさ)の2乗の長さをfloatで戻す = Playerが移動しているかどうかの確認
        if (dir.sqrMagnitude <= 0f)
        {
            return;
        }
        // 補間関数 Slerp（始まりの位置, 終わりの位置, 時間）　なめらかに回転する
        Vector3 forward = Vector3.Slerp(transform.forward, dir, rotateSpeed * Time.deltaTime);

        // 引数はVector3　Playerの向きを、自分を中心に変える
        transform.LookAt(transform.position + forward);
    }
}