using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *　カメラ用にステージの大きさを把握
 *　Floorにアタッチ
 */

public class StageRect : MonoBehaviour
{
    [SerializeField, Header("ステージの高さ(任意に設定可)")]
    private int mHeight = 10;

    [Header("ステージの範囲をRectで設定")]
    private Rect mStageRect;
    [Header("四隅の座標")]
    private Vector3 mTopL, mTopR, mBottomL, mBottomR;

    //ステージ範囲
    void OnDrawGizmos()
    {
        mBottomL = new Vector3(mStageRect.xMin, mStageRect.yMax, 0);
        mTopL = new Vector3(mStageRect.xMin, mStageRect.yMin, 0);
        mBottomR = new Vector3(mStageRect.xMax, mStageRect.yMax, 0);
        mTopR = new Vector3(mStageRect.xMax, mStageRect.yMin, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(mBottomL, mTopL);
        Gizmos.DrawLine(mTopL, mTopR);
        Gizmos.DrawLine(mTopR, mBottomR);
        Gizmos.DrawLine(mBottomR, mBottomL);
    }

    void Start()
    {
        RectUpdate();
    }

    void RectUpdate()
    {
        //地面のColliderを元にRectを設定
        Bounds floorBounds = GetComponent<Collider2D>().bounds;
        mStageRect.xMin = floorBounds.min.x;
        mStageRect.xMax = floorBounds.max.x;
        mStageRect.yMin = floorBounds.min.y;
        mStageRect.yMax = floorBounds.max.y + mHeight;
    }


    public Rect GetStageRect()
    {
        return mStageRect;
    }

    public void SetHeight(int height)
    {
        mHeight = height;
        RectUpdate();
    }
}
