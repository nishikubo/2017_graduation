using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnter : MonoBehaviour
{
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
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss
                = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss;
            Destroy(gameObject);
            print("enter");
        }
    }
}
