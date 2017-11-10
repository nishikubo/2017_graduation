using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵クラス②　回復アイテム落とす
/// </summary>
public class EnemyHeal : Enemy {

    //protected override void IdleState()
    //{
    //    base.IdleState();
    //}

    //protected override void WalkState(bool flip)
    //{

    //}

    //protected override void AttackState()
    //{
    //    base.AttackState();
    //}

    protected override void DeadState()
    {
        m_status.Dead();
        Destroy(this.gameObject);
        HealDrop();
    }

    /// <summary>
    /// 回復アイテムを落とす
    /// </summary>
    private void HealDrop()
    {
        //プレハブを取得
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Recovery");
        //プレハブからインスタンスを生成
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

}
