using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRandom : Fire {

	
	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        if (m_timer > 2.0f)
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

        if (col.gameObject.tag == "Slash" || col.gameObject.tag == "Fist")
        {
            Destroy(this.gameObject);
        }

    }

}
