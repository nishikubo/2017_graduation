using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// デバッグ確認用のプレイヤー（仮）クラス
/// </summary>
public class debugweapon : Scene {

    private EnemyManager m_enemyManager;

    public int at = 50;
    public int hp = 100;

    public Slider hpber;
    public bool gameover = false;

	// Use this for initialization
	void Start () {
        WeaponsList m_weapon = Scene.GetWeapon();
        Debug.Log("start: "+m_weapon);

        m_enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();

        
	}
	
	// Update is called once per frame
	void Update () {
        hpber.value = hp;

        if(hp<=0 && !gameover)
        {
            gameover = true;
            Debug.Log("Player死んだ");
            base.OnNext();
        }
    }

    public void DebugDamage(int damage)
    {
        hp = hp - damage;
        Debug.Log("プレイヤー：うぐっ…");
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        //攻撃があたったと仮定
        if (col.gameObject.tag == "Enemy")
        {
            //現状
            //col.gameObject.GetComponent<EnemyStatus>().Damage(at);
            //こっち
            //m_enemyManager.EnemyDamage(at, col.gameObject);
        }
    }
}
