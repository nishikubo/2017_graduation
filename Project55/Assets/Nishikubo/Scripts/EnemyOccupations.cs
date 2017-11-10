using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    NONE,
    PHYSICS,    //通常　徘徊タイプ（物理）
    MAGIC,      //魔法  位置固定タイプ（遠隔）
    HERD,       //群れタイプ
    DROP        //回復アイテムを落とすタイプ
}

/// <summary>
/// 後日削除予定
/// </summary>
public class EnemyOccupations : MonoBehaviour {

    public EnemyType State
    {
        get { return m_type; }
        set { m_type = value; }
    }

    [SerializeField]
    private EnemyType m_type = EnemyType.NONE;

    private EnemyState m_state;

    private EnemyStatus m_status;   //エネミーステータス


    private float m_attackTime = 0.0f;//攻撃時のタイム



    // Use this for initialization
    void Start () {
        m_status = this.GetComponent<EnemyStatus>();


    }

    // Update is called once per frame
    void Update () {

        if(EnemyState.ATTACK==m_state)
        {
            switch (m_type)
            {
                case EnemyType.NONE: break;
                case EnemyType.PHYSICS: PhysicsState(); break;
                case EnemyType.MAGIC: MagicState(); break;
                case EnemyType.HERD: HerdState(); break;
                case EnemyType.DROP: DropState(); break;
                default: break;
            }
        }
    }

    /// <summary>
    /// 物理
    /// </summary>
    public void PhysicsState()
    {
        Debug.Log("なぐったー");

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

    /// <summary>
    /// 遠隔
    /// </summary>
    public void MagicState()
    {
        Debug.Log("うったー");

        Debug.Log("攻撃した");

        GameObject prefab = (GameObject)Resources.Load("Prefabs/Fire");

        // プレハブからインスタンスを生成
        GameObject fire = Instantiate(prefab, new Vector3(transform.position.x, 0.0f, 0.0f), Quaternion.identity);
        //fire.transform.position = new Vector3(transform.position.x+5, fire.transform.position.y, 0);
        fire.transform.SetParent(transform, false);

        //fire.transform.DOMoveX(fire.transform.position.x - 5.0f, 2.0f);


        m_attackTime = 0;

    }

    /// <summary>
    /// 群れ
    /// </summary>
    public void HerdState()
    {
        Debug.Log("むれたー");
    }

    /// <summary>
    /// アイテム
    /// </summary>
    public void DropState()
    {
        Debug.Log("かいふくー");
    }
}
