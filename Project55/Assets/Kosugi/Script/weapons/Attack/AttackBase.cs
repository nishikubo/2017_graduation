﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *　攻撃の基礎
 */

public class AttackBase : MonoBehaviour {

    [SerializeField, Header("与ダメージ")]
    protected int mDamage = 0;

    [SerializeField, Header("消滅速度")]
    protected float mTime = 0.0f;

    [SerializeField, Header("向く方向")]
    protected Vector2 mVec;

    void Start () {
		
	}
	
	void Update () {
		
	}

    protected Vector3 SetDir(Vector3 dir)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        dir = player.transform.localScale;

        return dir;
    }
    protected Vector2 SetVec()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        return new Vector2(GameObject.FindGameObjectWithTag("Player").transform.localScale.x, 0);
    }
}
