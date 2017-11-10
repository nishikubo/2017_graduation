using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChange : MonoBehaviour {

    [SerializeField, Header("武器切り替えモードかどうか")]
    private bool mWeaponChange = false;
    [SerializeField, Header("選択した武器")]
    private WeaponsList mWeapon;

    [SerializeField, Header("移動量")]
    private Vector3 mInputVec = Vector3.zero;

    [Header("GameのState管理")]
    private StateManager mStateManager;

    [SerializeField, Header("背景")]
    private GameObject mBG;

    void Start () {
        mStateManager = GameObject.Find("GameManager").GetComponent<StateManager>();

        mWeapon = Scene.GetWeapon();

	}
	

	void Update () {
        Mode();
        Weapon();
	}

    /// <summary>
    /// 武器選択モード移行
    /// </summary>
    void Mode()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            mWeapon = WeaponsList.None;
            mStateManager.SetState(States.Weapon);
            mWeaponChange = true;
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            GetComponent<Player>().SetWeapon(mWeapon);
            mStateManager.SetState(States.Normal);
            mWeaponChange = false;
        }
    }
    /// <summary>
    /// 武器選択
    /// </summary>
    void Weapon()
    {
        mBG.SetActive(mWeaponChange);
        if (mStateManager.GetState() == States.Weapon)
        {
            mInputVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (mInputVec.y > 0)
            {
                mWeapon = WeaponsList.Sword;
            }
            else if (mInputVec.x > 0)
            {
                mWeapon = WeaponsList.Gun;
            }
            else if (mInputVec.y < 0)
            {
                mWeapon = WeaponsList.Fist;
            }
            else if (mInputVec.x < 0)
            {
                mWeapon = WeaponsList.Cane;
            }
        }
    }
}
