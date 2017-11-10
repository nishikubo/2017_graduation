using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : AttackBase {

    [SerializeField, Header("弾速")]
    private float mSpeed = 10.0f;

    private EnemyManager mEnemyManager;

    void Start () {
        SetDir(gameObject.transform.localScale);
        mVec = SetVec();

        mEnemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }
	
	void Update () {
        transform.position += new Vector3(mVec.x * mSpeed * Time.deltaTime, 0, 0);

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
            Destroy(gameObject);
        }
    }
}
