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

public class Player : MonoBehaviour
{
    //SerializeField TooltipAttribute Header

    [SerializeField, Header("移動速度")]
    private float mSpeed = 5.0f;
    [SerializeField, Header("移動量")]
    private Vector3 mInputVec = Vector3.zero;

    //[SerializeField, Header("壁判定用のレイの長さ")]
    private float mRayLengthWall = 1.0f;
    //[SerializeField, Header("床判定用のレイの長さ")]
    private float mRayLengthFloor = 0.2f;
    [SerializeField, Header("ジャンプ用の加える力")]
    private float mJumpPower = 250f;

    [SerializeField, Header("衝突し続けているか")]
    private bool mColFloor = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        //WallRay();
        //FloorRay();

        //print();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        mInputVec = new Vector3(Input.GetAxis("Horizontal") * mSpeed, 0, Input.GetAxis("Vertical") * mSpeed);

        transform.position += mInputVec * Time.deltaTime;
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (mColFloor && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector2.up * mJumpPower);
        }
    }

    /// <summary>
    /// 2Dメインで制作するので使わない予定
    /// Rayでの壁判定処理
    /// </summary>
    private void WallRay()
    {
        Ray mRayFront = new Ray(transform.position, mInputVec.normalized);

        RayHitInfo mRayInfo;
        RaycastHit mRayCast;
        //当たり判定を処理するレイヤーを指定
        int layermask = (1 << LayerMask.NameToLayer("Floor"));

        mRayInfo.isHit = Physics.Raycast(mRayFront, out mRayCast, mRayLengthWall, layermask, QueryTriggerInteraction.Ignore);

        if (mRayInfo.isHit)
        {

        }
        else
        {

        }

        Debug.DrawRay(transform.position, mInputVec.normalized * mRayLengthWall, Color.grey, 0.1f, false);
    }
    /// <summary>
    /// 2Dメインで制作するので使わない予定
    /// Rayでの床判定処理
    /// </summary>
    void FloorRay()
    {
        Ray mRayDown = new Ray(transform.position, -transform.up);

        RayHitInfo mRayInfo;
        RaycastHit mRayCast;
        //当たり判定を処理するレイヤーを指定
        int layermask = (1 << LayerMask.NameToLayer("Floor"));

        mRayInfo.isHit = Physics.Raycast(mRayDown, out mRayCast, mRayLengthFloor, layermask, QueryTriggerInteraction.Ignore);

        if (mRayInfo.isHit)
        {
           
        }

        Debug.DrawRay(transform.position, -transform.up * mRayLengthFloor, Color.red, 0.1f, false);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag=="Floor")
        {
            mColFloor = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            mColFloor = false;
        }
    }
}
