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
    //[SerializeField, Tooltip("体力ゲージ")]
    //protected Slider m_hpBer;                    

    /*移動時用*/
    [SerializeField, Tooltip("移動速度 右->'+' ,左->'-'")]
    protected float m_speed = 1.0f;
    protected float m_sign = 0.0f;//m_speedが＋－どちらであるかを保存用
    [SerializeField, Tooltip("移動距離")]
    protected float m_distance = 5.0f;
    protected float m_moved = 0.0f;//現在の座標保存用
    protected bool m_flip = false;    //反転させるか

    /*攻撃時用*/
    protected float m_attackTime = 0.0f;//攻撃時のタイム

    protected GameObject m_player;  //プレイヤー参照用


    // Use this for initialization
    protected void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        m_state = EnemyState.IDLE;
        m_status = this.GetComponent<EnemyStatus>();
        m_flip = this.GetComponent<SpriteRenderer>().flipX;

        //if(m_hpBer == null)
        //{
        //    m_hpBer = this.GetComponentInChildren<Slider>();
        //    m_hpBer.maxValue = m_status.GetHp();
        //}


        m_moved = transform.position.x;//現在のX座標
        m_sign = m_speed;//現在のスピード

        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected void Update () {
        //状態遷移
        switch (m_state)
        {
            case EnemyState.NONE:    break;
            case EnemyState.IDLE:    IdleState(); break;
            case EnemyState.WALK:    WalkState(m_flip); break;
            case EnemyState.ATTACK:  AttackState(); break;
            case EnemyState.DEAD:    DeadState(); break;
            default: break;
        }

        m_status.StatusUI();

        if (m_status.GetHp() <= 0)
        {
            m_state = EnemyState.DEAD;
        }

        //Debug_yo();

    }

    //待機状態
    protected virtual void IdleState()
    {
        //そのうち
        //カメラに入ったら動き出すとかに変えたい
        m_state = EnemyState.WALK;
    }

    //徘徊状態
    protected virtual void WalkState(bool flip)
    {
        //+だったら右へ
        if (m_sign > 0.0f)
        {
            if (Mathf.Floor(m_moved + m_distance) < Mathf.Floor(transform.position.x))
            {
                m_speed *= -1.0f;
            }
            else if (Mathf.Floor(transform.position.x) < Mathf.Floor(m_moved))
            {
                m_speed *= -1.0f;
            }
        }
        //-だったら左へ
        else if (0.0f > m_sign)
        {
            if (Mathf.Floor(m_moved + (-m_distance)) > Mathf.Floor(transform.position.x))
            {
                m_speed *= -1.0f;
            }
            else if (Mathf.Floor(transform.position.x) > Mathf.Floor(m_moved))
            {
                m_speed *= -1.0f;
            }
        }
        transform.Translate(Vector3.right * Time.deltaTime * m_speed);

        //反転させるか
        if (m_speed > 0.0f)
        {
            flip = true;
        }
        else if (m_speed < 0.0f)
        {
            flip = false;
        }
        GetComponent<SpriteRenderer>().flipX = flip;

    }

    //攻撃状態
    protected virtual void AttackState()
    {
        LookAtTarget(m_player,m_flip);


        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 3.0f)
        {
            Debug.Log("敵：えいっ！");
            //仮
            //m_player.GetComponent<debugweapon>().DebugDamage(m_status.Attack());
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


    protected virtual void DeadState()
    {
        m_status.Dead();
        Destroy(this.gameObject);
    }


    ///// <summary>
    ///// 関連UI表示
    ///// </summary>
    //protected void StatusUI()
    //{
    //    //HPバー
    //    m_hpBer.value = m_status.GetHp();
    //}

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
            m_flip = false;
        }
        else if (transform.position.x < target_posX)
        {
            m_flip = true;
        }
        GetComponent<SpriteRenderer>().flipX = flip;
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            Debug.Log("敵：えいっ！");
            //仮
            //m_player.GetComponent<debugweapon>().DebugDamage(m_status.Attack());
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

    ////プレイヤーに攻撃されたら
    ////とりあえず剣にattackタグをつけといた
    //protected void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "attack")
    //    {
    //        //m_state = EnemyState.ATTACK;
    //        Debug.Log("攻撃された");
    //        m_status.Damage(10);
    //    }
    //}



    //デバッグ用いろいろー
    public void Debug_yo()
    {

    }

}
