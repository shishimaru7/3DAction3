using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour
{
    [Header("死亡数")]
    private int deathCount;

    [Header("最大死亡数")]
    private int maxDeathCount;

    [Header("Bossのレベル")]
    private int bossLevel;

    [SerializeField]
    private Text bossLevelText;
    private Boss boss;

    private void Start()
    {
        bossLevel = 1;
        bossLevelText.text = "LV:" + bossLevel;
    }
    private void Update()
    {
        if (GameObject.Find("Boss") == null)
        {
            StartCoroutine(GoClearScene());
        }
    }
    IEnumerator GoClearScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Clear");
    }
    /// <summary>
    /// Monster撃破カウント
    /// </summary>
    /// <param name="amount"></param>
    public void MonsterCount(int amount)
    {
        deathCount += amount;
        BossLevelUp();
    }

    /// <summary>
    /// Monsterを指定数撃破するとボスのレベルが上がる
    /// HP、攻撃、防御のステータスが上がる
    /// </summary>
    private void BossLevelUp()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();

        //倒したカウント　＞＝　最大死亡数
        if (deathCount >= maxDeathCount)
        {
            bossLevel += 1;
            Debug.Log("レベル" + bossLevel);
            bossLevelText.text = "LV:" + bossLevel;

            //最大死亡数に達すると次に必要な最大死亡数が増える///この記述が無いとupdateの影響で能力上がり続ける
            maxDeathCount = Mathf.RoundToInt(maxDeathCount * 2f);

            boss.MaxHp = Mathf.RoundToInt(boss.MaxHp * 1.2f);
            Debug.Log("BossのHP" + boss.MaxHp + "up");

            boss.HP = boss.MaxHp;

            boss.EnemyAttackPower = Mathf.RoundToInt(boss.EnemyAttackPower + 5f);
            Debug.Log("Boss攻撃" + boss.EnemyAttackPower + "up");

            boss.DefensePower = Mathf.RoundToInt(boss.DefensePower + 5f);
            Debug.Log("Boss防御" + boss.DefensePower + "up");
        }
    }
}