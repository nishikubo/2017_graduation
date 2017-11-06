using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Default,
    Normal,
    Weapon,
    Pause,
    Clear,
    Death
}
public class StateManager : MonoBehaviour {

    private State mBeforeState;
    private State mNowState;

    void Start()
    {
        mBeforeState = State.Default;
        mNowState = State.Normal;
    }

    void Update()
    {

    }

    public void SetState(State state)
    {
        mBeforeState = mNowState;
        mNowState = state;
        if (mBeforeState == mNowState) return;

        print(mNowState);
    }

    public State GetMode()
    {
        return mNowState;
    }
}