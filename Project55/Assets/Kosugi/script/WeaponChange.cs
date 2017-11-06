using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour {

    [SerializeField, Header("武器切り替えモードかどうか")]
    private bool mWeaponChange = false;

    private StateManager mStateManager;

    [SerializeField, Header("背景")]
    private GameObject mBG;
    // Use this for initialization
    void Start () {
        mStateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
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
            mStateManager.SetState(State.Weapon);
            mWeaponChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            mStateManager.SetState(State.Normal);
            mWeaponChange = false;
        }
    }
    void Weapon()
    {
        //mBG.GetComponent<Image>().enabled = mWeaponChange;
        mBG.SetActive(mWeaponChange);
    }
}
