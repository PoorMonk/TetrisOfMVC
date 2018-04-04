using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState {

    private void Awake()
    {
        stateID = StateID.Play;
    }
}
