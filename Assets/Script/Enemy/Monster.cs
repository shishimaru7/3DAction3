using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : EnemyStatusBase
{
    [Header("死亡数")]
    private int die;

    [Header("アイテムドロップ")]
    [SerializeField]
   private GameObject item;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
        if (transform.position.y < -10)
        {
            gameObject.transform.position = new Vector3(0f, 10f, 0f);
        }
    }
    protected override void AttackStart()
    {
        base.AttackStart();
    }
    protected override void EnemyMove()
    {
        base.EnemyMove();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Death();
    }
    private void Death()
    {
        if (hp <= 0)
        {
            //倒したらカウントされる
            monsterManager.MonsterCount(die);

            if (Random.Range(0, 100) < 10)
            {
                Vector3 pos = transform.position;
                Instantiate(item, new Vector3(pos.x, 2, pos.z), Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    public override float PlayerAttackDamageAmount(float attackPower, float defense)
    {
        return base.PlayerAttackDamageAmount(attackPower, defense);
    }
    protected override void Poison()
    {
        base.Poison();
        Death();
    }
    protected override void Poisoncharge(float poison)
    {
        base.Poisoncharge(poison);
    }
}