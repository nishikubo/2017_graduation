using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField, Header("与ダメージ")]
    private int mDamage = 2;

    private Vector2 mVec;

    [SerializeField, Header("弾速")]
    private float mSpeed = 10.0f;

    [SerializeField, Header("消滅速度")]
    private float mTime = 2.0f;

    void Start () {
		
	}
	
	void Update () {
        transform.position += new Vector3(mVec.x * mSpeed * Time.deltaTime, 0, 0);

        mTime -= Time.deltaTime;
        if (mTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetVector(Vector2 vec)
    {
        mVec = vec;
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
