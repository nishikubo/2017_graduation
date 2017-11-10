using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵のステータスクラス
/// </summary>
public class EnemyStatus : MonoBehaviour {

    [SerializeField, Tooltip("攻撃力")]
    private int m_attack = 10;   //攻撃力
    [SerializeField, Tooltip("防御力")]
    private int m_defence = 5;   //防御力
    [SerializeField, Tooltip("体力")]
    private int m_hp = 100;      //体力
    private int m_maxHp = 0;     //体力最大値

    [SerializeField, Tooltip("体力ゲージ")]
    protected Slider m_hpBer;

    private void Start()
    {
        m_maxHp = GetHp();

        if (m_hpBer == null)
        {
            m_hpBer = this.GetComponentInChildren<Slider>();
        }

        m_hpBer.maxValue = m_maxHp;
        m_hpBer.value = m_maxHp;

    }

    public void SetAttack(int attack) { m_attack = attack; }
    public void SetDefence(int defence) { m_defence = defence; }
    public void SetHp(int hp) { m_hp = hp; }

    /// <summary>
    /// 攻撃力
    /// </summary>
    /// <returns>現在の攻撃力を返す</returns>
    public int GetAttack() { return m_attack; }
    /// <summary>
    /// 防御力
    /// </summary>
    /// <returns>現在の防御力を返す</returns>
    public int GetDefence() { return m_defence; }
    /// <summary>
    /// 体力
    /// </summary>
    /// <returns>現在の体力を返す</returns>
    public int GetHp() { return m_hp; }


    /// <summary>
    /// 通常攻撃
    /// </summary>
    /// <returns>(Enemy_AT)</returns>
    public int Attack()
    {
        return GetAttack();
    }


    /// <summary>
    /// 通常で攻撃を受けた
    /// </summary>
    /// <param name="damage">相手のステータス</param>
    public void Damage(int damage)
    {
        m_hp = (int)(Mathf.Max(0, m_hp - (damage - GetDefence())));

        //StartCoroutine(Blink(0.1f));

    }

    //IEnumerator Blink(float interval)
    //{
    //    SpriteRenderer renderer = GetComponent<SpriteRenderer>();

    //    while (true)
    //    {
    //        renderer.enabled = !renderer.enabled;
    //        yield return new WaitForSeconds(interval);
    //    }
    //}



    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Dead()
    {
        m_hp = 0;
    }

    /// <summary>
    /// 関連UI表示
    /// </summary>
    public void StatusUI()
    {
        //HPバー
        m_hpBer.value = GetHp();
    }


}
