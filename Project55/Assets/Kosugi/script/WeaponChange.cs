using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mode();
	}

    void Mode()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            print("Down");
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            print("Up");
        }
    }
}
