using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : WeaponBase
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !mIsRecast)
        {
            Attack(mAtkPrefub, mAtkPos, true);
            mIsRecast = true;
            mTime = mRecastTime;
        }

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
