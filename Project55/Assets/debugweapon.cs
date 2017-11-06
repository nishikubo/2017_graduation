using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugweapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Weapon _weapon = Scene.getWeapon();
        Debug.Log(_weapon);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
