using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : AttackBase {

    private EnemyManager mEnemyManager;

    void Start()
    {
        mVec = SetVec();

        mEnemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    void Update()
    {
        mTime -= Time.deltaTime;
        if (mTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            mEnemyManager.EnemyDamage(mDamage, col.gameObject);
        }
    }
}
