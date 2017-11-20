using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour {

    private EnemyManager m_enemyManager;

	// Use this for initialization
	void Start () {
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            m_enemyManager.EnemyDead(col.gameObject);
        }
    }

}
