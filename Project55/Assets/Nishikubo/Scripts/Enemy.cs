using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    None,
    Physics,    //通常　徘徊タイプ（物理）
    Magic,      //魔法  位置固定タイプ（遠隔）
    Herd,       //群れタイプ
    ItemDrop    //回復アイテムを落とすタイプ
}


public enum EnemyState
{
    None,   
    Idle,   //待機
    Walk,   //徘徊
    Attack, //攻撃
    Dead    //死亡
}

public class Enemy : MonoBehaviour {
    public EnemyState State
    {
        get { return _state; }
        set { _state = value; }
    }

    [SerializeField]
    private EnemyState _state = EnemyState.None;  //プレイヤーの遷移

    private EnemyStatus _status;   //エネミーステータス
    [SerializeField, Tooltip("体力ゲージ")]
    private Slider _hpBer;                     //体力ゲージ


    [SerializeField, Tooltip("移動速度　0.1とか")]
    private Vector3 speed = Vector3.zero;
    [SerializeField, Tooltip("移動距離")]
    private Vector3 distance = Vector3.zero;
    private Vector3 moved = Vector3.zero;


    private bool flag = false;
    private float time = 0.0f;//攻撃時のタイム

    [SerializeField, Tooltip("回復アイテムを落とすか")]
    private bool _item = false;//アイテムドロップをするかしないか


    // Use this for initialization
    void Start () {
        _state = EnemyState.Idle;
        _status = this.GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update () {
        //攻撃とかの遷移
        switch (_state)
        {
            case EnemyState.None:    break;
            case EnemyState.Idle:    IdleState(); break;
            case EnemyState.Walk:    WalkState(); break;
            case EnemyState.Attack:  AttackState(); break;
            case EnemyState.Dead:    DeadState(); break;
            default: break;
        }

        Debug_yo();

        if (_status.GetHp() <= 0)
        {
            _state = EnemyState.Dead;
        }
    }

    //待機状態
    private void IdleState()
    {
        //そのうち
        //カメラに入ったら動き出すとかに変えたい
        _state = EnemyState.Walk;
    }

    //徘徊状態
    private void WalkState()
    {
        //左右上下移動
        float x = speed.x;
        float y = speed.y;

        if (moved.x >= distance.x)
        {
            x = 0;
        }
        else if (moved.x + speed.x > distance.x)
        {
            x = distance.x - moved.x;
        }
        if (moved.y >= distance.y)
        {
            y = 0;
        }
        else if (moved.y + speed.y > distance.y)
        {
            y = distance.y - moved.y;
        }
        transform.Translate(x, y, 0);
        moved.x += Mathf.Abs(speed.x);
        moved.y += Mathf.Abs(speed.y);

        //Vector3 Scale = transform.localScale;
        bool flip = GetComponent<SpriteRenderer>().flipX;
        
        if (moved.x >= distance.x && moved.y >= distance.y)
        {
            speed *= -1;
            moved = Vector3.zero;
            //Scale.x *= -1;
            //flip = false;


        }
        if(speed.x>=0)
        {
            flip = true;

        }
        else if(speed.x<0)
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
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Debug.Log("攻撃した");
            time = 0;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("攻撃された");
            _status.damage(10);
            //攻撃受けたらフラグ立てて
            //何秒かは攻撃しない
        }

    }

    //private IEnumerator attack(bool damage=false)
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
        _status.dead();
        Destroy(this.gameObject);
        if(_item==true)
        {
            ItemDrop();
        }
    }

    //関連UI表示
    private void StatusUI()
    {
        //HPバー
        _hpBer.value = _status.GetHp();
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
            //flag = true;
            _state = EnemyState.Attack;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //flag = false;
            _state = EnemyState.Idle;
        }
    }

    //プレイヤーに攻撃されたら
    //とりあえず剣にattackタグをつけといた
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "attack")
        {
            _state = EnemyState.Attack;
        }
    }



    //デバッグ用いろいろー
    private void Debug_yo()
    {

    }

}
