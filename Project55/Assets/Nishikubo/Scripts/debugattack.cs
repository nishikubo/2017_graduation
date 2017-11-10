using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugattack : debugweapon {

    private EnemyManager m_enemyManager;


    // Use this for initialization
    void Start () {
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("プレイヤー剣：くらえ！！" + base.at);
            
            m_enemyManager.EnemyDamage(base.at, col.gameObject);
        }
    }

}
