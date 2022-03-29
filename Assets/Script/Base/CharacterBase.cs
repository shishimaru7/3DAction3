using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Playerと敵の基底クラス
/// </summary>
public class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    public float HP { set { hp = value; hp = Mathf.Clamp(hp, 0, maxHp); } get { return hp; } }
    [SerializeField]
    protected float maxHp;
    public float MaxHp { set { maxHp = value; } get { return maxHp; } }
    [SerializeField]
    protected float defensePower;
    public float DefensePower { set { defensePower = value; } get { return defensePower; } }

    [Header("移動速度")]
    [SerializeField]
    protected float moveSpeed;
    protected Vector3 dir;

    protected Rigidbody rb;

    protected Animator animator;
}