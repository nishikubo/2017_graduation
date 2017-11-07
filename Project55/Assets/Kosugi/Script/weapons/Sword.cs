using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    [SerializeField, Header("斬撃プレハブ")]
    private GameObject mSlashPrefub;

    [SerializeField, Header("攻撃ポイント")]
    private GameObject mPoint;

    [SerializeField, Header("消費HP")]
    private int mWeaponHP = 4;

    void Start()
    {

    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject mBullet = (GameObject)Instantiate(mSlashPrefub, mPoint.transform.position, Quaternion.identity);
        }
    }

    public float GetWeaponHP()
    {
        return mWeaponHP;
    }
}
