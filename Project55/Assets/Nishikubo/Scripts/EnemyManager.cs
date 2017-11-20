using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵管理クラス
/// </summary>
public class EnemyManager : Scene {

    [SerializeField, Tooltip("ステージ内の敵設定")]
    private List<GameObject> m_enemyList;

    //ダメージの管理、死亡処理

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// ダメージを与える
    /// </summary>
    /// <param name="damage">自身の攻撃力</param>
    /// <param name="target">ダメージを減らす相手</param>
    public void EnemyDamage(int damage,GameObject target /*,int num*/)
    {
        ////属性ごとに判定
        //switch (num)
        //{
        //    case 0: target.GetComponent<EnemyStatus>().Damage(damage); break;
        //    case 1:
        //        if () { target.GetComponent<EnemyStatus>().Damage(damage); }
        //         break;
        //    default: break;
        //}
        //ターゲット（敵）にもintをせっていしておく
        //それと同じ？だったら弱点 swixh必要ないか

        target.GetComponent<EnemyStatus>().Damage(damage);
        Debug.Log("敵：ぐはっ…");
    }

    /// <summary>
    /// 雑魚敵死亡時
    /// </summary>
    /// <param name="obj">自身</param>
    public void EnemyDead(GameObject obj)
    {
        //敵死亡時リストから削除
        m_enemyList.Remove(obj);
        Destroy(obj);
    }

    /// <summary>
    /// ゲームクリア時
    /// </summary>
    /// <param name="boss"></param>
    public void GameClear(GameObject boss)
    {
        m_enemyList.Remove(boss);
        Destroy(boss);
        //リザルトへ
        //OnNext();
        OnClear();
    }
}
