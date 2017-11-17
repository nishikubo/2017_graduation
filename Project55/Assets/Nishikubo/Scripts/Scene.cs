using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン全体
/// 追加する際はシーンの名前と一致させること
/// </summary>
public enum SceneType
{
    Title,
    Select,
    StageSelect,
    Credit,
    Rezult,
    Main/*,
    Stage01,
    Stage02*/
}

/// <summary>
/// 武器選択
/// 後日別クラスに移動予定
/// </summary>
public enum Weapon
{
    NONE,
    SWORD,
    GUN,
    FIST,
    CANE
}

/// <summary>
/// シーン遷移クラス
/// </summary>
public class Scene : MonoBehaviour {

    [SerializeField,Tooltip("飛ぶシーン選択")]
    private SceneType m_currentScene = SceneType.Title;

    [SerializeField,Tooltip("対象武器選択(使わないときはNone)")]
    private WeaponsList m_weapon = WeaponsList.None;
    public static WeaponsList m_weaponCheck;  //保持


    // Update is called once per frame
    //void Update()
    //{
    //    switch(m_currentScene)
    //    {
    //        case SceneType.StageSelect: NextStage(); BeforeStage(); break;
    //        default: break;
    //    }
    //}


    /// <summary>
    /// 次のシーンへ
    /// </summary>
    public void OnNext()
    {
        SceneNavigator.Instance.Change(m_currentScene.ToString(), 1.5f);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 武器選択
    /// </summary>
    /// <param name="weapon"></param>
    public void OnWeapon()
    {
        
        m_weaponCheck = m_weapon;
        SceneNavigator.Instance.Change(m_currentScene.ToString(), 1.5f);

    }

    /// <summary>
    /// 武器保持
    /// </summary>
    /// <returns></returns>
    public static WeaponsList GetWeapon()
    {
        return m_weaponCheck;
    }

    /// <summary>
    /// 次のステージ選択
    /// </summary>
    //public void NextStage()
    //{
    //    if(Input.GetButtonDown("Right"))
    //    {
    //        Debug.Log("right");
    //    }
    //}

    /// <summary>
    /// 前のステージ選択
    /// </summary>
    //public void BeforeStage()
    //{
    //    if (Input.GetButtonDown("Left"))
    //    {
    //        Debug.Log("reft");
    //    }
    //}


}
