using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状態ごとにスクリプトを起動するよう書く
public class StateManager : MonoBehaviour {

    private States mBeforeState;
    private States mNewState;

    void Start()
    {
        mBeforeState = States.None;
        mNewState = States.Normal;
    }

    void Update()
    {

    }

    public void SetState(States state)
    {
        mBeforeState = mNewState;
        mNewState = state;
        if (mBeforeState == mNewState) return;

        if (mNewState == States.Normal)
        {
            Time.timeScale = 1;
        }
        else if (mNewState == States.Weapon)
        {
            Time.timeScale = 0;
        }
    }

    public States GetState()
    {
        return mNewState;
    }
}