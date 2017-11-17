using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ボスの状態遷移
/// </summary>
public enum BossState
{
    NONE,
    IDLE,   //待機
    ATTACK, //攻撃
    DEAD    //死亡

}

public enum Attack
{
    PHYSICS,    //物理
    PLAYER,     //プレイヤー追尾
    LONGLANGE,  //遠距離
    MAGIC       //魔法
    //群れ
    //HP依存で


    //プレイヤーが接近してきたら

    //プレイヤーが遠隔にいたら
}

/// <summary>
/// ボスクラス①
/// </summary>
public class EnemyBoss : MonoBehaviour
{
    public BossState State
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField]
    protected BossState m_state = BossState.NONE;  //遷移

    protected EnemyStatus m_status;   //ステータス

    protected GameObject m_player;  //プレイヤー参照用
    protected EnemyManager m_enemyManager;

    /*攻撃時用*/
    protected float m_attackTime = 0.0f;//攻撃時のタイム



    protected void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
    }

    // Use this for initialization
    protected void Start()
    {

        m_state = BossState.IDLE;
        m_status = this.GetComponent<EnemyStatus>();
        //m_flip = this.GetComponent<SpriteRenderer>().flipX;

        m_player = GameObject.FindGameObjectWithTag("Player");
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();

    }

    // Update is called once per frame
    protected void Update()
    {
        //状態遷移
        switch (m_state)
        {
            case BossState.NONE: break;
            case BossState.IDLE: IdleState(); break;
            case BossState.ATTACK: AttackState(); break;
            case BossState.DEAD: DeadState(); break;
            default: break;
        }

        m_status.StatusUI();

        if (m_status.GetHp() <= 0)
        {
            m_state = BossState.DEAD;
        }



        
    }

    /// <summary>
    /// 待機状態
    /// </summary>
    protected virtual void IdleState()
    {
        //そのうち
        //カメラに入ったら動き出すとかに変えたい
        m_state = BossState.ATTACK;
    }

    /// <summary>
    /// 攻撃状態
    /// </summary>
    protected void AttackState()
    {
        AttackFire();


    }


    /// <summary>
    /// 死亡状態
    /// </summary>
    protected virtual void DeadState()
    {
        m_status.Dead();
        //Destroy(this.gameObject);

        //ステージクリア
        m_enemyManager.GameClear(this.gameObject);
    }


    public void AttackFire()
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Fire");

        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 3.0f)
        {
            //m_player.GetComponent<Player>().PlayerDamage(m_status.Attack());

            // プレハブからインスタンスを生成
            GameObject fire = Instantiate(prefab);
            //fire.transform.position = new Vector3(transform.position.x+5, fire.transform.position.y, 0);
            fire.transform.SetParent(transform, false);

            fire.transform.DOMoveX(m_player.transform.position.x, 2.0f);


            m_attackTime = 0;
        }

    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("敵：えいっ！");
            m_player.GetComponent<Player>().PlayerDamage(m_status.Attack());

            //m_state = BossState.ATTACK;
        }
    }

    protected void OnCollisionExit2D(Collision2D col)
    {
        //if (col.gameObject.tag == "Player")
        //{
        //    m_state = BossState.IDLE;
        //}
    }

}
