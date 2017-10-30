using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour {

    [SerializeField, Header("武器切り替えモードかどうか")]
    private bool mWeaponChange = false;


    [SerializeField, Header("背景")]
    private GameObject mBG;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mode();
        Weapon();
	}

    void Mode()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            mWeaponChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            mWeaponChange = false;
        }
    }
    void Weapon()
    {
        //mBG.GetComponent<Image>().enabled = mWeaponChange;
        mBG.SetActive(mWeaponChange);
    }
}
