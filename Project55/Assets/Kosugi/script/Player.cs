using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField, Header("HP")]
    private float mHP = 100.0f;
    [SerializeField, Header("HP用UI")]
    private Slider mHP_UI;

    [SerializeField, Header("移動速度")]
    private float mSpeed = 5.0f;
    [SerializeField, Header("移動量")]
    private Vector3 mInputVec = Vector3.zero;

    [SerializeField, Header("ジャンプ力")]
    private float mJumpPower = 4.0f;
    [Header("重力")]
    private float mGravity = 9.8f;
    [SerializeField, Header("床と接しているか")]
    private bool mColFloor = false;

    [SerializeField, Header("装備中の武器")]
    private WeaponsList mNewWeapon;
    [SerializeField, Header("1つ前の武器")]
    private WeaponsList mBeforeWeapon;

    [SerializeField, Header("武器スクリプトのアクティブ化用")]
    private Dictionary<WeaponsList, MonoBehaviour> mWeaponScript;
    [SerializeField, Header("武器リソースのアクティブ化用")]
    private GameObject[] mWeaponResource;

    [Header("GameのState管理")]
    private StateManager mStateManager;

    void Start()
    {
        //取得
        mStateManager = GameObject.Find("GameManager").GetComponent<StateManager>();

        //設定
        mNewWeapon = Scene.StartWeapon();
        mBeforeWeapon = WeaponsList.None;
        mWeaponScript = new Dictionary<WeaponsList, MonoBehaviour>()
        {
            {WeaponsList.Sword, GetComponent<Sword>()   },
            {WeaponsList.Gun,   GetComponent<Gun>()     },
            {WeaponsList.Fist,  GetComponent<Fist>()    },
            {WeaponsList.Cane,  GetComponent<Cane>()    }
        };

        //最初の武器リソースをアクティブ
        ActiveSetting();
    }

    void Update()
    {
        Move();
        Jump();

        Active(mNewWeapon);
        PlayerStatus();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        if (mStateManager.GetState() != States.Normal) return;

        mInputVec = new Vector3(Input.GetAxis("Horizontal") * mSpeed, 0);
        transform.position += mInputVec * Time.deltaTime;

        if (mInputVec.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (mInputVec.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (mColFloor && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * mJumpPower * 100);
        }

        if (!mColFloor)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * mGravity);
        }
    }

    /// <summary>
    /// 武器スクリプトのアクティブ化設定
    /// </summary>
    void Active(WeaponsList weapon)
    {
        foreach (KeyValuePair<WeaponsList, MonoBehaviour> attack in mWeaponScript)
        {
            if (weapon == attack.Key)
                //move.Valueからだと変更できないみたい
                mWeaponScript[attack.Key].enabled = true;
            else
                mWeaponScript[attack.Key].enabled = false;
        }
    }
    /// <summary>
    /// 武器
    /// </summary>
    /// <param name="weapon"></param>
    public void SetWeapon(WeaponsList weapon)
    {
        if (weapon == WeaponsList.None || weapon == mNewWeapon) return;

        mBeforeWeapon = mNewWeapon;
        mNewWeapon = weapon;

        if (mNewWeapon == WeaponsList.Sword)
        {
            mHP -= mWeaponScript[WeaponsList.Sword].GetComponent<Sword>().GetWeaponHP();
        }
        if (mNewWeapon == WeaponsList.Gun)
        {
            mHP -= mWeaponScript[WeaponsList.Gun].GetComponent<Gun>().GetWeaponHP();
        }

        ActiveSetting();
    }

    /// <summary>
    /// プレイヤーのステータス管理
    /// </summary>
    void PlayerStatus()
    {
        mHP_UI.value = mHP;

        if (mHP <= 0)
        {
            mStateManager.SetState(States.Death);
        }
    }
    /// <summary>
    /// 武器リソースのアクティブ化設定
    /// </summary>
    void ActiveSetting()
    {
        for (int i = 0; i < mWeaponResource.Length; i++)
        {
            mWeaponResource[i].GetComponent<ActiveSelect>().Active(mBeforeWeapon.ToString(), mNewWeapon.ToString());
        }
    }


    /*----当たり判定関連----*/
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            mHP -= col.gameObject.GetComponent<EnemyStatus>().GetAttack();
        }
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
