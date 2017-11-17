using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//前方範囲？プレイヤー中心範囲？前後範囲？　要検討

/*
 *　杖
 *　Playerにアタッチ
 */

public class Cane : WeaponBase
{
    [SerializeField, Header("詠唱時間")]
    private float mCastTime = 0.0f;

    /*詠唱中は動けない？*/

    void Start () {
		
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.K) && !mIsRecast)
        {
            mCastTime += Time.deltaTime;

            if (mCastTime >= 2.0f)
            {
                Attack(mAtkPrefub, mAtkPos, false);
                mIsRecast = true;
                mTime = mRecastTime;
                mCastTime = 0;
            }
        }
        else
            mCastTime = 0;

        if (mIsRecast)
        {
            mTime -= Time.deltaTime;
            if (mTime <= 0)
            {
                mIsRecast = false;
                mTime = mRecastTime;
            }
        }
    }
}
