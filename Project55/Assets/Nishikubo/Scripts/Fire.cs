using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    private EnemyStatus m_status;

    void Start()
    {
        m_status = transform.root.gameObject.GetComponent<EnemyStatus>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("敵：えいっ！");
            col.GetComponent<Player>().PlayerDamage(m_status.Attack());
            Destroy(this.gameObject);
            
        }
    }

}
