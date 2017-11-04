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


public class Scene : MonoBehaviour {

    private SceneType _currentScene = SceneType.Title;
    //https://gametukurikata.com/program/data シーン遷移参考にして


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
