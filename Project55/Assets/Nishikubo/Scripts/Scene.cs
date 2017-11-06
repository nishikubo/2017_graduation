using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

public enum Weapon
{
    None,
    Sword,
    Gun,
    Fist,
    Cane
}



public class Scene : MonoBehaviour {

    [SerializeField,Tooltip("飛ぶシーン選択")]
    private SceneType _currentScene = SceneType.Title;
    //https://gametukurikata.com/program/data シーン遷移参考にして

    [SerializeField]
    private Weapon _weapon = Weapon.None;
    public static Weapon _weaponCheck;


    /// <summary>
    /// 次のシーンへ
    /// </summary>
    public void OnNext()
    {
        SceneNavigator.Instance.Change(_currentScene.ToString(), 1.5f);
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnWeapon(string weapon)
    {
        
        _weaponCheck = _weapon;
        weapon = _weaponCheck.ToString();
        Debug.Log(weapon);
        SceneNavigator.Instance.Change(_currentScene.ToString(), 1.5f);

    }

    public static Weapon getWeapon()
    {
        return _weaponCheck;
    }

}
