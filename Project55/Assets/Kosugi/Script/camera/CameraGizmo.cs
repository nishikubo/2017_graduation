using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGizmo : MonoBehaviour
{
    [Header("カメラの表示領域")]
    private Vector3 mCameraTopL, mCameraTopR, mCameraBottomL, mCameraBottomR;

    [SerializeField, Header("カメラの横、縦")]
    private float mCameraWidth, mCameraHeight;

    void Start ()
	{
        
    }
	
	void Update ()
	{
        SizeSet();
    }

    void SizeSet()
    {
        if (mCameraWidth != 0 || mCameraHeight != 0) return;

        mCameraWidth = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GetCameraWidthSize();
        mCameraHeight = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().GetCameraHeightSize();
    }

    void OnDrawGizmos()
    {
        mCameraTopL = new Vector2(transform.position.x - mCameraWidth/2, transform.position.y + mCameraHeight/2);
        mCameraTopR = new Vector2(transform.position.x + mCameraWidth / 2, transform.position.y + mCameraHeight / 2);
        mCameraBottomL = new Vector2(transform.position.x - mCameraWidth / 2, transform.position.y - mCameraHeight / 2);
        mCameraBottomR = new Vector2(transform.position.x + mCameraWidth / 2, transform.position.y - mCameraHeight / 2);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(mCameraBottomL, mCameraTopL);
        Gizmos.DrawLine(mCameraTopL, mCameraTopR);
        Gizmos.DrawLine(mCameraTopR, mCameraBottomR);
        Gizmos.DrawLine(mCameraBottomR, mCameraBottomL);
    }
}
