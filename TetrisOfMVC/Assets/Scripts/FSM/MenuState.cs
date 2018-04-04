using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState {

    private void Awake()
    {
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick, StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        m_ctrl.m_view.ShowMenu();
        m_ctrl.m_cameraControl.ZoomOut();
    }

    public override void DoBeforeLeaving()
    {
        m_ctrl.m_view.HideMenu();
    }

    public void OnStartButtonClick()
    {
        m_fsmState.PerformTransition(Transition.StartButtonClick);
    }
}
