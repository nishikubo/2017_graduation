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

    //protected GameObject prefab;//プレハブ読み込み用
    //protected GameObject prefab1;
    //protected GameObject prefab2;

    protected GameObject[] prefabs = new GameObject[3];

    protected int r = 0;    //攻撃のランダム用変数

    //public bool m_attack = false;


    protected GameObject m_bossHP;
    public bool bossFlag = false;

    protected void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        LoadResources();
    }

    // Use this for initialization
    protected void Start()
    {
        m_bossHP = GameObject.Find("BossHP");
        m_bossHP.SetActive(false);//初期は非表示

        m_status = this.GetComponent<EnemyStatus>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();

        m_state = BossState.IDLE;

    }

    // Update is called once per frame
    protected void Update()
    {
        if(bossFlag==true)
        {
            m_bossHP.SetActive(true);//ボス面に入ったら表示
        }


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
        r = Random.Range(0, 3);
        m_state = BossState.ATTACK;
    }

    /// <summary>
    /// 攻撃状態
    /// </summary>
    protected void AttackState()
    {

        switch (r)
        {
            case 0: AttackFire(); break;
            case 1: PlayerTracking(); break;
            case 2: SetMagicPillar(); break;
            /*case 3: Debug.Log("何もしない"); break;*/
            /*case 4: Debug.Log("何もしない"); break;*/
            default: break;
        }
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


    //炎出す
    public void AttackFire()
    {
        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 5.0f)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject fire = Instantiate(prefabs[1]);
                fire.transform.SetParent(transform, false);
                fire.transform.DOMove(Random.insideUnitCircle * 3, 2.0f);
            }

            m_attackTime = 0;
            m_state = BossState.IDLE;
        }
    }

    //プレイヤー追尾する
    public void PlayerTracking()
    {
        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 5.0f)
        {
            // プレハブからインスタンスを生成
            GameObject fire = Instantiate(prefabs[0]);
            fire.transform.SetParent(transform, false);
            fire.transform.DOMove(new Vector3(m_player.transform.position.x, m_player.transform.position.y), 2.0f);
            
            m_attackTime = 0;
            m_state = BossState.IDLE;
        }
    }

    //魔法陣設置後火柱
    public void SetMagicPillar()
    {
        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 5.0f)
        {
            GameObject fire = Instantiate(prefabs[2]);
            fire.transform.SetParent(transform, false);
            fire.transform.position = new Vector3(m_player.transform.position.x, fire.transform.position.y, 0);

            m_attackTime = 0;
            m_state = BossState.IDLE;
        }
    }


    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("敵：えいっ！");
            m_player.GetComponent<Player>().PlayerDamage(m_status.Attack());

        }
    }


    //リソース読み込み
    public void LoadResources()
    {
        prefabs[0] = (GameObject)Resources.Load("Prefabs/Fire");
        prefabs[1] = (GameObject)Resources.Load("Prefabs/FireRand");
        prefabs[2] = (GameObject)Resources.Load("Prefabs/PillarOfFire");

    }

}
