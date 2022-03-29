using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
    /// 銃口は弾薬の発射間隔と時間と弾替え
    /// </summary>
public class NormalShotPoint : MonoBehaviour
{
    [Header("銃弾")]
    [SerializeField]
    private GameObject[] bulletprefab;

    [Header("発射間隔")]
    [SerializeField]
    private float span;

    [Header("時間")]
    [SerializeField]
    private float time;

    [Header("弾速")]
    [SerializeField]
    private float bulltSpeed;

    //弾変えるカウント
    private int currentBulletNumber;

    // 弾を発射する位置(ShotPointゲームオブジェクトを登録)
    [SerializeField]
    private Transform shotTransform;
    [SerializeField]
    private Text bulletText;


   public AudioSource audioSource;
    public AudioClip changeSE;

    // 弾の種類
    public enum BulletType
    {
        通常弾,    // 0
        消滅弾,    // 1
        貫通弾,    // 2
        毒弾       // 3
    }

    private void Start()
    {
        // 初期値設定
        currentBulletNumber = 0;

        // 最初に装備している弾の名称を表示
        bulletText.text = (BulletType)currentBulletNumber + "装備中";
    }

    void Update()
    {
        //時間進める
        time += Time.deltaTime;

        // 弾を変更(ここにif文があった方が処理が読みやすいです)
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeBullet();
        }

        //間隔が時間超えたらボタンで動く
        if (Input.GetKeyDown(KeyCode.Z) && span <= time)
        {
            // 弾を生成
            CreateBullet();
        }
    }

    /// <summary>
    /// 現在選択されている弾を生成
    /// </summary>
    public void CreateBullet()
    {
        //時間は０に戻る
        time = 0;

        //インスタンシエトしたものをゲームオブジェクト型の変数に代入(numberがここでしか使われていませんので、currentBulletNumberにします)
        GameObject bullet = Instantiate(bulletprefab[currentBulletNumber], shotTransform.position, Quaternion.identity);

        //代入したものにリジッドボディ―をゲットコンポ　velocity = 自身.正面　*　弾速
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulltSpeed;
    }

    /// <summary>
    /// 弾を変更
    /// </summary>
    void ChangeBullet()
    {
        // 弾の番号を変更
        currentBulletNumber++;

        // 弾がプレファブの最大値を超えたら0に戻す
        if (currentBulletNumber >= bulletprefab.Length)
        {
            currentBulletNumber = 0;
        }

        // 弾の変更に合わせて、弾の番号をEnumにキャスト(型変換)して表示する文字を変更
        bulletText.text = (BulletType)currentBulletNumber + "装備中";

        // 同じ処理は1回にしましょう
        audioSource.PlayOneShot(changeSE);
    }
}
