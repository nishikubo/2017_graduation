using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPillar : MonoBehaviour {

    private EnemyStatus m_status;

    private float m_timer = 0.0f;

    // Use this for initialization
    void Start () {
        m_status = transform.root.gameObject.GetComponent<EnemyStatus>();

    }

    // Update is called once per frame
    void Update () {
        m_timer += Time.deltaTime;
        if(m_timer>2.0f)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;

            if (m_timer>5.0f)
            {
                m_timer = 0;
                Destroy(transform.parent.gameObject);
            }
        }

	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Player>().PlayerDamage(m_status.Attack());
        }

    }

}
