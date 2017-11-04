using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

    [SerializeField, Tooltip("攻撃力")]
    private int _attack = 10;   //攻撃力
    [SerializeField, Tooltip("防御力")]
    private int _defence = 5;   //防御力
    [SerializeField, Tooltip("体力")]
    private int _hp = 100;      //体力
    private int _maxHp = 0;     //体力最大値

    private void Start()
    {
        _maxHp = GetHp();
    }

    public void SetAttack(int attack) { _attack = attack; }
    public void SetDefence(int defence) { _defence = defence; }
    public void SetHp(int hp) { _hp = hp; }

    /// <summary>
    /// 攻撃力
    /// </summary>
    /// <returns>現在の攻撃力を返す</returns>
    public int GetAttack() { return _attack; }
    /// <summary>
    /// 防御力
    /// </summary>
    /// <returns>現在の防御力を返す</returns>
    public int GetDefence() { return _defence; }
    /// <summary>
    /// 体力
    /// </summary>
    /// <returns>現在の体力を返す</returns>
    public int GetHp() { return _hp; }


    /// <summary>
    /// 通常攻撃
    /// </summary>
    /// <returns>(Enemy_AT)</returns>
    public int attack()
    {
        return GetAttack();
    }


    /// <summary>
    /// 通常で攻撃を受けた
    /// </summary>
    /// <param name="damage">相手のステータス</param>
    public void damage(int damage)
    {
        _hp = (int)(Mathf.Max(0, _hp - (damage - GetDefence())));

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
    public void dead()
    {
        _hp = 0;
    }

}
