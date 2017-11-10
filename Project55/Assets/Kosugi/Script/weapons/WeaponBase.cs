using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField, Header("攻撃判定用プレハブ")]
    protected GameObject mAtkPrefub;

    [SerializeField, Header("攻撃ポイントの座標")]
    protected Transform mAtkPos;

    [SerializeField, Header("消費HP")]
    protected int mWeaponHP = 0;

    [SerializeField, Header("リキャストタイム")]
    protected float mRecastTime = 0.0f;
    [SerializeField, Header("リキャストタイム(変動用)")]
    protected float mTime = 0.0f;

    [SerializeField, Header("リキャストが必要か")]
    public bool mIsRecast = false;

    void Start()
    {

    }

    void Update()
    {

    }

    protected void Attack(GameObject prefub, Transform pos, bool parent)
    {
        GameObject mAttack = (GameObject)Instantiate(prefub, pos.position, Quaternion.identity);

        //trueの場合プレイヤーの子オブジェクトにする
        if (parent)
            mAttack.transform.parent = transform;
    }
    public float GetWeaponHP()
    {
        return mWeaponHP;
    }
}
