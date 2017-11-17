using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵クラス①　指定した位置に移動する
/// </summary>
public class EnemyNormal : Enemy {

    private float m_startPosX = 0.0f;   //スタート位置取得
    private float m_endPosX = 0.0f;     //エンド位置取得

    protected override void Start()
    {
        m_state = EnemyState.IDLE;
        m_status = this.GetComponent<EnemyStatus>();
        m_flip = this.GetComponent<SpriteRenderer>().flipX;

        m_player = GameObject.FindGameObjectWithTag("Player");

        m_startPosX = transform.parent.FindChild("StartPos").transform.position.x;
        m_endPosX = transform.parent.Find("EndPos").transform.position.x ;

        m_enemyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();


    }

    protected override void WalkState()
    {
        //左右移動
        if (m_startPosX < m_endPosX)
        {
            if (m_endPosX <= transform.position.x)
            {
                m_flip = false;
                m_endPosX = Mathf.Round(m_startPosX);
                m_startPosX = Mathf.Round(transform.position.x);
            }
            transform.Translate(Vector3.right * Time.deltaTime * m_speed);
        }
        else if(m_endPosX < m_startPosX)
        {
            if (m_endPosX >= transform.position.x)
            {
                m_flip = true;
                m_endPosX = Mathf.Round(m_startPosX);
                m_startPosX = Mathf.Round(transform.position.x);
            }
            transform.Translate(Vector3.left * Time.deltaTime * m_speed);
        }

        GetComponent<SpriteRenderer>().flipX = m_flip;
    }


}
