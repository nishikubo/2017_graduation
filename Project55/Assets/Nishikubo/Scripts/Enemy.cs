using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 敵の状態遷移
/// </summary>
public enum EnemyState
{
    NONE,   
    IDLE,   //待機
    WALK,   //徘徊
    ATTACK, //攻撃
    DEAD    //死亡
}

/// <summary>
/// 敵ベースクラス
/// </summary>
public class Enemy : MonoBehaviour {
    public EnemyState State
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField]
    protected EnemyState m_state = EnemyState.NONE;  //エネミーの遷移

    protected EnemyStatus m_status;   //エネミーステータス

    /*移動時用*/
    [SerializeField, Tooltip("移動速度 右->'+' ,左->'-'")]
    protected float m_speed = 1.0f;
    protected bool m_flip = false;    //反転させるか

    /*攻撃時用*/
    protected float m_attackTime = 0.0f;//攻撃時のタイム

    protected GameObject m_player;  //プレイヤー参照用

    protected EnemyManager m_enemyManager;

    protected void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    // Use this for initialization
    protected virtual void Start () {

        m_state = EnemyState.IDLE;
        m_status = this.GetComponent<EnemyStatus>();
        m_flip = this.GetComponent<SpriteRenderer>().flipX;

        m_player = GameObject.FindGameObjectWithTag("Player");
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();

    }

    // Update is called once per frame
    protected void Update () {
        //状態遷移
        switch (m_state)
        {
            case EnemyState.NONE:    break;
            case EnemyState.IDLE:    IdleState(); break;
            case EnemyState.WALK:    WalkState(); break;
            case EnemyState.ATTACK:  AttackState(); break;
            case EnemyState.DEAD:    DeadState(); break;
            default: break;
        }

        m_status.StatusUI();

        if (m_status.GetHp() <= 0)
        {
            m_state = EnemyState.DEAD;
        }

    }

    /// <summary>
    /// 待機状態
    /// </summary>
    protected virtual void IdleState()
    {
        //そのうち
        //カメラに入ったら動き出すとかに変えたい
        m_state = EnemyState.WALK;
    }

    /// <summary>
    /// 徘徊状態
    /// </summary>
    protected virtual void WalkState()
    {
        //まっすぐ直進
        if(m_flip)
        {
            transform.Translate(Vector3.right * Time.deltaTime * m_speed);
        }
        else if(!m_flip)
        {
            transform.Translate(Vector3.left * Time.deltaTime * m_speed);
        }
    }

    /// <summary>
    /// 攻撃状態
    /// </summary>
    protected virtual void AttackState()
    {
        LookAtTarget(m_player,m_flip);

        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 3.0f)
        {
            Debug.Log("敵：えいっ！");
            m_player.GetComponent<Player>().PlayerDamage(m_status.Attack());

            m_attackTime = 0;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("敵：ぐはっ…");
            m_status.Damage(10);
            //攻撃受けたらフラグ立てて
            //何秒かは攻撃しない
        }

    }

    /// <summary>
    /// 死亡状態
    /// </summary>
    protected virtual void DeadState()
    {
        m_status.Dead();
        m_enemyManager.EnemyDead(this.gameObject);
    }


    /// <summary>
    /// 対象の方向に反転
    /// </summary>
    /// <param name="target">向いてほしい対象</param>
    /// <param name="flip">向き反転</param>
    protected void LookAtTarget(GameObject target, bool flip)
    {
        float target_posX = target.transform.position.x;
        //敵より＋かーか　向き反転
        if (transform.position.x > target_posX)
        {
            flip = false;
        }
        else if (transform.position.x < target_posX)
        {
            flip = true;
        }
        GetComponent<SpriteRenderer>().flipX = flip;
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            Debug.Log("敵：えいっ！");
            m_player.GetComponent<Player>().PlayerDamage(m_status.Attack());

            m_state = EnemyState.ATTACK;
        }
    }

    protected void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_state = EnemyState.IDLE;
        }
    }

    //デバッグ用いろいろー
    public void Debug_yo()
    {

    }

}
