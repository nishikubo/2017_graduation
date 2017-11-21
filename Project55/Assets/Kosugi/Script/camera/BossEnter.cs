using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
    [Header("プレイヤー侵入判定用")]
    private bool mPlayerEnter;

    void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<BossAreaManager>().SetEnter(true);
            Destroy(gameObject);
        }
    }
}
