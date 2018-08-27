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
        m_ctrl.m_audioManager.PlayCursor();
        m_fsmState.PerformTransition(Transition.StartButtonClick);
        //m_ctrl.m_view.ShowGameUI();
    }

    public void OnReStartMenuButtonClick()
    {
        m_ctrl.m_audioManager.PlayCursor();
        m_fsmState.PerformTransition(Transition.StartButtonClick);
        m_ctrl.m_model.LoadData();
        m_ctrl.m_view.OnRestartButtonClick();
    }
}
