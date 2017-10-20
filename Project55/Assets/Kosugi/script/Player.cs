using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rayの当たり判定用
/// </summary>
struct RayHitInfo
{
    public RaycastHit hit;
    //当たったか？
    public bool isHit;
};
/// <summary>
/// 左右どちらを向いているかの判定用
/// </summary>
public enum Direction
{
    LEFT,
    RIGHT
};
public class Player : MonoBehaviour
{
    //SerializeField TooltipAttribute Header

    [SerializeField, Header("移動速度")]
    private float mSpeed = 2.0f;
    [SerializeField, Header("移動量")]
    private Vector3 mInputVec = Vector3.zero;

    [SerializeField, Header("当たり判定用のレイの長さ")]
    private float mWallRayLength = 0.2f;
    [SerializeField, Header("ジャンプ用の加える力")]
    private float mJumpPower = 300f;

    [SerializeField, Header("衝突し続けているか")]
    private bool mColContinues = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        WallRay();
        FloorRay();
        //print();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        mInputVec = new Vector3(Input.GetAxis("Horizontal") * mSpeed, 0, Input.GetAxis("Vertical") * mSpeed);

        if (!mColContinues)
        {
            transform.position += mInputVec * Time.deltaTime;
        }
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * mJumpPower);
        }
    }

    /// <summary>
    /// Rayでの壁判定処理
    /// </summary>
    private void WallRay()
    {
        Ray mRayFront = new Ray(transform.position, mInputVec.normalized);

        RayHitInfo mRayInfo;
        RaycastHit mRayCast;
        //当たり判定を処理するレイヤーを指定
        int layermask = (1 << LayerMask.NameToLayer("Floor"));

        mRayInfo.isHit = Physics.Raycast(mRayFront, out mRayCast, mWallRayLength, layermask, QueryTriggerInteraction.Ignore);

        if (mRayInfo.isHit)
        {
            mColContinues = true;
        }
        else
        {
            mColContinues = false;
        }

        Debug.DrawRay(transform.position, mInputVec.normalized, Color.grey, 0.1f, false);
    }
    /// <summary>
    /// Rayでの床判定処理
    /// </summary>
    void FloorRay()
    {
        Ray mRayDown = new Ray(transform.position, -transform.up);

        RayHitInfo mRayInfo;
        RaycastHit mRayCast;
        //当たり判定を処理するレイヤーを指定
        int layermask = (1 << LayerMask.NameToLayer("Floor"));

        mRayInfo.isHit = Physics.Raycast(mRayDown, out mRayCast, mWallRayLength, layermask, QueryTriggerInteraction.Ignore);

        Debug.DrawRay(transform.position, -transform.up, Color.red, 0.1f, false);
    }

    private void OnCollisionStay(Collision col)
    {

    }
    private void OnCollisionExit(Collision col)
    {

    }
}
