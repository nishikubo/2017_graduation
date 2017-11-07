using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField, Header("銃弾プレハブ")]
    private GameObject mBulletPrefub;

    [SerializeField, Header("攻撃ポイント")]
    private GameObject mPoint;

    [SerializeField, Header("消費HP")]
    private int mWeaponHP = 4;

    void Start () {

	}
	
	void Update () {
        Attack();
	}

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject mBullet=(GameObject)Instantiate(mBulletPrefub, mPoint.transform.position, Quaternion.identity);
            mBullet.GetComponent<Bullet>().SetVector(new Vector2(transform.localScale.x, 0));
        }
    }

    public float GetWeaponHP()
    {
        return mWeaponHP;
    }
}
