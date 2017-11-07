using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

    [SerializeField, Header("与ダメージ")]
    private int mDamage = 6;

    [SerializeField, Header("消滅速度")]
    private float mTime = 2.0f;

    void Start()
    {

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
            col.gameObject.GetComponent<Enemy>().Damage(mDamage);
            Destroy(gameObject);
        }
    }
}
