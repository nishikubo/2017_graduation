using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HP : MonoBehaviour {

    [SerializeField, Header("回復力")]
    private int mHealPoint = 0;

	void Start () {
		
	}
	
	void Update () {
		
	}

    void Heal(GameObject obj)
    {
        obj.GetComponent<Player>().PlayerDamage(-mHealPoint);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Heal(col.gameObject);
            Destroy(gameObject);
        }
    }
}
