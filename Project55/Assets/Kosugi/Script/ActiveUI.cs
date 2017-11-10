using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUI : MonoBehaviour {

    [SerializeField, Header("選択した場所")]
    private Vector3 mInputVec = Vector3.zero;

    [SerializeField, Header("選択肢"), TooltipAttribute("0:上, 1:右, 2:下, 3:左")]
    private GameObject[] mSelectObjects;
    [SerializeField, Header("選択前のUI画像")]
    private  Sprite[] mBeforeImage;
    [SerializeField, Header("選択後のUI画像")]
    private Sprite[] mIAfterImage;

    private int mNum;

    [Header("GameのState管理")]
    private StateManager mStateManager;

    void Start () {
        switch (Scene.GetWeapon())
        {
            case WeaponsList.Sword:
                mNum = 0;
                break;
            case WeaponsList.Gun:
                mNum = 1;
                break;
            case WeaponsList.Fist:
                mNum = 2;
                break;
            case WeaponsList.Cane:
                mNum = 3;
                break;
        }
	}
	
	void Update () {
        ActiveSelect();
	}

    void ActiveSelect()
    {
        mInputVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (mInputVec.y > 0)
        {
            mNum = 0;
        }
        else if (mInputVec.x > 0)
        {
            mNum = 1;
        }
        else if (mInputVec.y < 0)
        {
            mNum = 2;
        }
        else if (mInputVec.x < 0)
        {
            mNum = 3;
        }
        Set(mNum);
    }

    void Set(int num)
    {
        switch (num)
        {
            case 0:
                mSelectObjects[0].GetComponent<Image>().sprite = mIAfterImage[0];
                mSelectObjects[1].GetComponent<Image>().sprite = mBeforeImage[1];
                mSelectObjects[2].GetComponent<Image>().sprite = mBeforeImage[2];
                mSelectObjects[3].GetComponent<Image>().sprite = mBeforeImage[3];
                break;
            case 1:
                mSelectObjects[0].GetComponent<Image>().sprite = mBeforeImage[0];
                mSelectObjects[1].GetComponent<Image>().sprite = mIAfterImage[1];
                mSelectObjects[2].GetComponent<Image>().sprite = mBeforeImage[2];
                mSelectObjects[3].GetComponent<Image>().sprite = mBeforeImage[3];
                break;
            case 2:
                mSelectObjects[0].GetComponent<Image>().sprite = mBeforeImage[0];
                mSelectObjects[1].GetComponent<Image>().sprite = mBeforeImage[1];
                mSelectObjects[2].GetComponent<Image>().sprite = mIAfterImage[2];
                mSelectObjects[3].GetComponent<Image>().sprite = mBeforeImage[3];
                break;
            case 3:
                mSelectObjects[0].GetComponent<Image>().sprite = mBeforeImage[0];
                mSelectObjects[1].GetComponent<Image>().sprite = mBeforeImage[1];
                mSelectObjects[2].GetComponent<Image>().sprite = mBeforeImage[2];
                mSelectObjects[3].GetComponent<Image>().sprite = mIAfterImage[3];
                break;
        }
    }
}
