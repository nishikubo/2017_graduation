using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : AttackBase
{

    void Start()
    {
        mVec = SetVec();
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
            col.gameObject.GetComponent<EnemyStatus>().Damage(mDamage);
        }
    }
}
