using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    NONE,
    PHYSICS,    //通常　徘徊タイプ（物理）
    MAGIC,      //魔法  位置固定タイプ（遠隔）
    HERD,       //群れタイプ
    DROP        //回復アイテムを落とすタイプ
}


public enum EnemyState
{
    NONE,   
    IDLE,   //待機
    WALK,   //徘徊
    ATTACK, //攻撃
    DEAD    //死亡
}

public class Enemy : MonoBehaviour {
    public EnemyState State
    {
        get { return m_state; }
        set { m_state = value; }
    }

    [SerializeField]
    private EnemyState m_state = EnemyState.NONE;  //プレイヤーの遷移

    private EnemyStatus m_status;   //エネミーステータス
    [SerializeField, Tooltip("体力ゲージ")]
    private Slider m_hpBer;                     //体力ゲージ


    [SerializeField, Tooltip("移動速度　0.1とか")]
    private Vector3 m_speed = Vector3.zero;
    [SerializeField, Tooltip("移動距離")]
    private Vector3 m_distance = Vector3.zero;
    private Vector3 m_moved = Vector3.zero;

    private float m_attackTime = 0.0f;//攻撃時のタイム

    [SerializeField, Tooltip("回復アイテムを落とすか")]
    private bool m_item = false;//アイテムドロップをするかしないか


    // Use this for initialization
    void Start () {
        m_state = EnemyState.IDLE;
        m_status = this.GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update () {
        //攻撃とかの遷移
        switch (m_state)
        {
            case EnemyState.NONE:    break;
            case EnemyState.IDLE:    IdleState(); break;
            case EnemyState.WALK:    WalkState(); break;
            case EnemyState.ATTACK:  AttackState(); break;
            case EnemyState.DEAD:    DeadState(); break;
            default: break;
        }

        Debug_yo();

        if (m_status.GetHp() <= 0)
        {
            m_state = EnemyState.DEAD;
        }
    }

    //待機状態
    private void IdleState()
    {
        //そのうち
        //カメラに入ったら動き出すとかに変えたい
        m_state = EnemyState.WALK;
    }

    //徘徊状態
    private void WalkState()
    {
        //左右上下移動
        float x = m_speed.x;
        float y = m_speed.y;

        if (m_moved.x >= m_distance.x)
        {
            x = 0;
        }
        else if (m_moved.x + m_speed.x > m_distance.x)
        {
            x = m_distance.x - m_moved.x;
        }
        if (m_moved.y >= m_distance.y)
        {
            y = 0;
        }
        else if (m_moved.y + m_speed.y > m_distance.y)
        {
            y = m_distance.y - m_moved.y;
        }
        transform.Translate(x, y, 0);
        m_moved.x += Mathf.Abs(m_speed.x);
        m_moved.y += Mathf.Abs(m_speed.y);

        //Vector3 Scale = transform.localScale;
        bool flip = GetComponent<SpriteRenderer>().flipX;
        
        if (m_moved.x >= m_distance.x && m_moved.y >= m_distance.y)
        {
            m_speed *= -1;
            m_moved = Vector3.zero;
            //Scale.x *= -1;
            //flip = false;


        }
        if(m_speed.x>=0)
        {
            flip = true;

        }
        else if(m_speed.x<0)
        {
            flip = false;

        }

        //transform.localScale = Scale;
        GetComponent<SpriteRenderer>().flipX = flip;



    }

    //攻撃状態
    private void AttackState()
    {
        StatusUI();

        //数秒ごとに攻撃
        m_attackTime += Time.deltaTime;
        if (m_attackTime > 5.0f)
        {
            Debug.Log("攻撃した");
            m_attackTime = 0;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("攻撃された");
            m_status.Damage(10);
            //攻撃受けたらフラグ立てて
            //何秒かは攻撃しない
        }

    }

    //private IEnumerator Attack(bool damage=false)
    //{
    //    //プレイヤーを参照


    //    // ループ
    //    while (true)
    //    {
    //            // 1秒毎にループします
    //            yield return new WaitForSeconds(5f);
    //        onTimer();

    //    }
    //}

    //private void onTimer()
    //{
    //    Debug.Log("プレイヤーに攻撃している");
    //}


    //死亡状態

    private void DeadState()
    {
        m_status.Dead();
        Destroy(this.gameObject);
        if(m_item==true)
        {
            ItemDrop();
        }
    }

    //関連UI表示
    private void StatusUI()
    {
        //HPバー
        m_hpBer.value = m_status.GetHp();
        //GameObject.Find("EnemyCanvas").transform.LookAt(GameObject.Find("Main Camera").transform);
    }

    //アイテム
    private void ItemDrop()
    {
        //たおしたときに回復アイテム落とす
        // プレハブを取得
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Recovery");
        // プレハブからインスタンスを生成
        Instantiate(prefab, transform.position, Quaternion.identity);
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            m_state = EnemyState.ATTACK;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            m_state = EnemyState.IDLE;
        }
    }

    //プレイヤーに攻撃されたら
    //とりあえず剣にattackタグをつけといた
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "attack")
        {
            m_state = EnemyState.ATTACK;
        }
    }



    //デバッグ用いろいろー
    private void Debug_yo()
    {

    }

}
