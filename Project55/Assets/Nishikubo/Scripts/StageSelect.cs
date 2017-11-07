using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    [SerializeField,Tooltip("ステージの最大個数")]
    private int m_maxNum = 3;    //ステージの最大
    private GameObject m_frame; //親参照用
    private GameObject[] m_prefab;   //生成したプレファブ

    private GameObject m_center;

    private bool flag = false;//Dotween中か


    void Awake()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        m_frame = GameObject.Find("Frame");
        m_maxNum += 1;//’Stage0'の分

        m_prefab = new GameObject[m_maxNum];
    }

    // Use this for initialization
    void Start () {

        for (int i = 0; i < m_maxNum; i++)
        {
            m_prefab[i] = Instantiate((GameObject)Resources.Load("Prefabs/Stage" + i.ToString()),new Vector3(i*500.0f,0.0f,0.0f),Quaternion.identity);
            m_prefab[i].transform.SetParent(m_frame.transform, false);
        }

        m_center = m_prefab[0];

    }

    // Update is called once per frame
    void Update () {


        if (Input.GetButtonDown("Right"))
        {

            Debug.Log("right");
            //右押しても右にはいかない
            if (m_prefab[m_maxNum - 1].GetComponent<RectTransform>().localPosition.x <= 0)
            {
                Debug.Log("oooooooo");
            }
            else if(!flag)
            {
                StartCoroutine("sleep");

                for (int i = 0; i < m_maxNum; i++)
                {
                    //m_prefab[i].GetComponent<RectTransform>().localPosition = new Vector3(m_prefab[i].GetComponent<RectTransform>().localPosition.x - 500.0f, m_prefab[i].GetComponent<RectTransform>().localPosition.y, m_prefab[i].GetComponent<RectTransform>().localPosition.z);
                    m_prefab[i].GetComponent<RectTransform>().DOLocalMoveX(m_prefab[i].GetComponent<RectTransform>().localPosition.x - 500.0f, 1.0f);

                }
            }
        }

        if (Input.GetButtonDown("Left"))
        {
            Debug.Log("reft");

            //左押しても左にはいかない
            if (m_prefab[0].GetComponent<RectTransform>().localPosition.x >= 0)
            {
                Debug.Log("aaaaaa");
            }
            else if(!flag)
            {
                StartCoroutine("sleep");

                for (int i = 0; i < m_maxNum; i++)
                {
                    //m_prefab[i].GetComponent<RectTransform>().localPosition = new Vector3(m_prefab[i].GetComponent<RectTransform>().localPosition.x + 500.0f, m_prefab[i].GetComponent<RectTransform>().localPosition.y, m_prefab[i].GetComponent<RectTransform>().localPosition.z);
                    m_prefab[i].GetComponent<RectTransform>().DOLocalMoveX(m_prefab[i].GetComponent<RectTransform>().localPosition.x + 500.0f, 1.0f);

                }
            }
        }

        for (int i = 0; i < m_maxNum; i++)
        {
            if(m_prefab[i].GetComponent<RectTransform>().localPosition.x == 0)
            {
                m_center = m_prefab[i];
            }
        }
        
    }

    IEnumerator sleep()
    {
        Debug.Log("開始");
        flag = true;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("1.5秒経ちました");
        flag = false;
   }
}
