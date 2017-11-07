using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSelect : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void Active(string beforeName, string newName)
    {
        gameObject.transform.FindChild(beforeName).gameObject.SetActive(false);
        gameObject.transform.FindChild(newName).gameObject.SetActive(true);
    }
}
