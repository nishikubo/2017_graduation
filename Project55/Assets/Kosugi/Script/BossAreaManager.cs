using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaManager : MonoBehaviour
{
    [SerializeField, Header("ボスエリアの壁用オブジェクト")]
    private GameObject mBossWall;

    [SerializeField, Header("ボスエリアの出入り用フラグ")]
    private bool mEnter,mExit;

	void Start ()
	{
		
	}
	
	void Update ()
	{
        if (mEnter)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss
                = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss;
            mBossWall.SetActive(true);

            mEnter = false;
        }
        if (mExit)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss
                = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().mBoss;

            mBossWall.SetActive(false);

            mExit = false;
        }
	}

    /// <summary>
    /// ボスエリア侵入
    /// </summary>
    public void SetEnter(bool flag)
    {
        mEnter = flag;
    }
    /// <summary>
    /// ボスエリア退出
    /// </summary>
    public void SetExit(bool flag)
    {
        mExit = flag;
    }
}
