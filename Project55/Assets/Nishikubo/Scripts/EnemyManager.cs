using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵管理クラス
/// </summary>
public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_enemyList;

    //private EnemyStatus m_enemyStatus;

    //ダメージの管理、死亡処理

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnemyDamage(int damage,GameObject target)
    {
        target.GetComponent<EnemyStatus>().Damage(damage);
        Debug.Log("敵：ぐはっ…");
    }
}
