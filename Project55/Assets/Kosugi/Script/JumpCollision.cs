using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollision : MonoBehaviour
{
    private GameObject mPlayer;

	void Start ()
	{
        mPlayer = transform.parent.gameObject;
	}
	
	void Update ()
	{
		
	}

    /*----当たり判定関連----*/
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            mPlayer.GetComponent<Player>().SetJumpFlag(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            mPlayer.GetComponent<Player>().SetJumpFlag(false);
        }
    }
}
