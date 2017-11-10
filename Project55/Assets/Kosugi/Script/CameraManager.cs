using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField, Header("カメラが移動しなくなる最小の座標")]
    private Vector3 mMinPos;

    [SerializeField, Header("カメラが移動しなくなる最大の座標")]
    private Vector3 mMaxPos;

    [SerializeField, Header("ボスの座標")]
    private Vector3 mBossPos;


    private GameObject mPlayer;
    void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        CameraMove();
	}

    void CameraMove()
    {
        Vector3 pos = new Vector3(mPlayer.transform.position.x, transform.position.y, transform.position.z);
        if (mMinPos.x < mPlayer.transform.position.x && mPlayer.transform.position.x < mMaxPos.x)
        {
            transform.position = pos;
        }
    }
}
