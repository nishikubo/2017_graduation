using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGizmo : MonoBehaviour
{
    [Header("カメラの表示領域")]
    private Vector3 mCameraTopL, mCameraTopR, mCameraBottomL, mCameraBottomR;

    void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

    void OnDrawGizmos()
    {
        mCameraTopL = new Vector2(transform.position.x - 10, transform.position.y + 5);
        mCameraTopR = new Vector2(transform.position.x + 10, transform.position.y + 5);
        mCameraBottomL = new Vector2(transform.position.x - 10, transform.position.y - 5);
        mCameraBottomR = new Vector2(transform.position.x + 10, transform.position.y - 5);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(mCameraBottomL, mCameraTopL);
        Gizmos.DrawLine(mCameraTopL, mCameraTopR);
        Gizmos.DrawLine(mCameraTopR, mCameraBottomR);
        Gizmos.DrawLine(mCameraBottomR, mCameraBottomL);
    }
}
