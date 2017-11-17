using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  カメラ位置の調整と移動制限
 *  MainCameraにアタッチ
 */

public class CameraController : MonoBehaviour
{
    [Header("プレイヤー")]
    private GameObject mPlayer;
    [Header("プレイヤーとカメラの差異")]
    private Vector3 mOffset;
    [Header("Stage(Floor)")]
    private StageRect mStageRect;
    [Header("プレイヤーまでの距離")]
    private float mDistance = 10f;

    [Header("カメラの表示領域")]
    private Vector3 mCameraTopL, mCameraTopR, mCameraBottomL, mCameraBottomR;

    [SerializeField, Header("カメラの横幅")]
    private float mCameraWidth;
    [SerializeField, Header("カメラの縦幅")]
    private float mCameraHeight;

    [Header("カメラの座標")]
    private Vector3 mNewPosition;
    [Header("移動制限座標")]
    private Vector3 mLimitPosition;

    [Header("ボス突入フラグ")]
    public bool mBoss;

    [Header("Stateの管理")]
    private StateManager mStateManager;

    /// <summary>
    /// カメラの表示領域を緑ラインで表示
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(mCameraBottomL, mCameraTopL);
        Gizmos.DrawLine(mCameraTopL, mCameraTopR);
        Gizmos.DrawLine(mCameraTopR, mCameraBottomR);
        Gizmos.DrawLine(mCameraBottomR, mCameraBottomL);
    }

    void Start()
    {
        //取得
        mStateManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<StateManager>();

        //プレイヤーキャラを取得
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mOffset = transform.position - mPlayer.transform.position;
        //StageRectを取得
        mStageRect = GameObject.Find("Floor").GetComponent<StageRect>();
    }

    

    void LateUpdate()
    {
        if (!mBoss)
            NormaCamera();
        else
            BossCamera();
    }
    void NormaCamera()
    {
        Rect mRect = mStageRect.GetStageRect();
        float mNewX = 0f;
        float mNewY = 0f;

        //プレイヤーキャラの位置にカメラの座標を設定する。キャラのちょっと上にする
        mNewPosition = mPlayer.transform.position + mOffset + Vector3.up * 3f;

        CameraSetting();

        //カメラの稼働領域をステージ領域に制限
        mNewX = Mathf.Clamp(mNewPosition.x, mRect.xMin + mCameraWidth / 2, mRect.xMax - mCameraWidth / 2);
        mNewY = Mathf.Clamp(mNewPosition.y, 0, mRect.yMax - mCameraHeight / 2);

        //座標をカメラ位置に設定
        mLimitPosition = new Vector3(mNewX, mNewY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, mLimitPosition, 5.0f * Time.deltaTime);
    }
    void BossCamera()
    {
        Vector3 mBossPos= GameObject.Find("BossCameraPos").transform.position;
        mLimitPosition = new Vector3(mBossPos.x, mBossPos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, mLimitPosition, 3.0f * Time.deltaTime);
    }
    /// <summary>
    /// ビューポート座標をワールド座標に変換
    /// </summary>
    void CameraSetting()
    {
        mCameraBottomL = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, mDistance));
        mCameraTopR = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, mDistance));
        mCameraTopL = new Vector3(mCameraBottomL.x, mCameraTopR.y, mCameraBottomL.z);
        mCameraBottomR = new Vector3(mCameraTopR.x, mCameraBottomL.y, mCameraTopR.z);
        mCameraWidth = Vector3.Distance(mCameraBottomL, mCameraBottomR);
        mCameraHeight = Vector3.Distance(mCameraBottomL, mCameraTopL);
    }
}
