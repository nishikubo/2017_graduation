using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 普通の炎/追尾あり/跳ね返せる
/// </summary>
public class Fire : MonoBehaviour {

    protected EnemyStatus m_status;
    protected EnemyManager m_enemyManager;

    private bool m_counter = false;//自分がダメージを受けるか

    protected float m_timer = 0.0f;

    protected void Start()
    {
        m_status = transform.root.gameObject.GetComponent<EnemyStatus>();
        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 2.0f && !m_counter)
        {
            m_timer = 0;
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Player>().PlayerDamage(m_status.Attack());
            Destroy(this.gameObject);           
        }

        if((col.gameObject.tag == "Slash" || col.gameObject.tag == "Fist") && !m_counter)
        {
            m_counter = true;
            transform.DOMove(new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y), 2.0f);
            transform.parent = null;
        }

        if (col.gameObject.tag == "Enemy" && m_counter)
        {
            m_enemyManager.EnemyDamage(m_status.Attack(), col.gameObject);
            Destroy(this.gameObject);
        }
    }

}
