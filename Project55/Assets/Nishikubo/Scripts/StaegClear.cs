using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaegClear : Scene {

    private bool m_gameClear = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !m_gameClear)
        {
            m_gameClear = true;
            Debug.Log("clear");
            base.OnNext();
        }
    }
}
