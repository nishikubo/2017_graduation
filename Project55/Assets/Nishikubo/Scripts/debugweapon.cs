using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugweapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Weapon m_weapon = Scene.GetWeapon();
        Debug.Log(m_weapon);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
