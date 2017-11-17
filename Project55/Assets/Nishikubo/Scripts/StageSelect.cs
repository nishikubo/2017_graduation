using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// ステージセレクトクラス
/// ステージを選ぶ際に呼ばれる
/// </summary>
public class StageSelect : MonoBehaviour {

    [SerializeField,Tooltip("ステージの最大個数")]
    private int m_maxNum = 3;    //ステージの最大
    private GameObject m_frame; //親参照用
    private GameObject[] m_prefab;   //生成したプレファブ

    private GameObject m_center;//真ん中に表示されてるステージ

    private bool m_playing = false;//Dotween中か

    private GameObject m_selected;


    void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        m_frame = GameObject.Find("Frame");
        m_maxNum += 1;//’Stage0'の分

        m_prefab = new GameObject[m_maxNum];

        m_selected = GameObject.Find("EventSystem").GetComponent<EventSystem>().firstSelectedGameObject;

    }

    // Use this for initialization
    void Start () {

        for (int i = 0; i < m_maxNum; i++)
        {
            m_prefab[i] = Instantiate((GameObject)Resources.Load("Prefabs/Stage" + i.ToString()),new Vector3(i*500.0f,0.0f,0.0f),Quaternion.identity);
            m_prefab[i].transform.SetParent(m_frame.transform, false);
        }

        m_center = m_prefab[0];

        m_selected = m_center.GetComponentInChildren<Button>().gameObject;
        GameObject.Find("EventSystem").GetComponent<EventSystem>().firstSelectedGameObject = m_selected;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Right"))
        {
            NextStage();
        }
        if (Input.GetButtonDown("Left"))
        {
            BeforeStage();
        }



        for (int i = 0; i < m_maxNum; i++)
        {
            if (m_prefab[i].GetComponent<RectTransform>().localPosition.x == 0)
            {
                m_center = m_prefab[i];
            }
        }

    }

    /// <summary>
    /// 右押すと反応
    /// </summary>
    public void NextStage()
    {
        //右押しても右にはいかない
        if (m_prefab[m_maxNum - 1].GetComponent<RectTransform>().localPosition.x <= 0.0f)
        {
            //これ以上いかない
        }
        else if (!m_playing)
        {
            StartCoroutine("sleep");

            for (int i = 0; i < m_maxNum; i++)
            {
                //m_prefab[i].GetComponent<RectTransform>().localPosition = new Vector3(m_prefab[i].GetComponent<RectTransform>().localPosition.x - 500.0f, m_prefab[i].GetComponent<RectTransform>().localPosition.y, m_prefab[i].GetComponent<RectTransform>().localPosition.z);
                m_prefab[i].GetComponent<RectTransform>().DOLocalMoveX(m_prefab[i].GetComponent<RectTransform>().localPosition.x - 500.0f, 1.0f);
            }
        }
    }

    /// <summary>
    /// 左押すと反応
    /// </summary>
    public void BeforeStage()
    {
        //左押しても左にはいかない
        if (m_prefab[0].GetComponent<RectTransform>().localPosition.x >= 0.0f)
        {
            //これ以上いかない
        }
        else if (!m_playing)
        {
            StartCoroutine("sleep");

            for (int i = 0; i < m_maxNum; i++)
            {
                //m_prefab[i].GetComponent<RectTransform>().localPosition = new Vector3(m_prefab[i].GetComponent<RectTransform>().localPosition.x + 500.0f, m_prefab[i].GetComponent<RectTransform>().localPosition.y, m_prefab[i].GetComponent<RectTransform>().localPosition.z);
                m_prefab[i].GetComponent<RectTransform>().DOLocalMoveX(m_prefab[i].GetComponent<RectTransform>().localPosition.x + 500.0f, 1.0f);
            }
        }
    }

    /// <summary>
    /// カウントしてる間は反応させない
    /// </summary>
    /// <returns></returns>
    IEnumerator sleep()
    {
        Debug.Log("開始");
        m_playing = true;
        yield return new WaitForSeconds(1.2f);
        WeaponSelected();
        Debug.Log("1.5秒経ちました");
        m_playing = false;
   }


    private void WeaponSelected()
    {
        //Debug.Log("select  " + m_selected);
        //Debug.Log("center  " + m_center + "  carrent  " + m_center.GetComponentInChildren<Button>());

        m_selected = m_center.GetComponentInChildren<Button>().gameObject;
        m_selected.GetComponent<Button>().Select();
    }

}
