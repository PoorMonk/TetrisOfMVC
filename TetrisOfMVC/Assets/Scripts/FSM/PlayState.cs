using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState {

    private void Awake()
    {
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }

    public override void DoBeforeEntering()
    {
        m_ctrl.m_view.ShowGameUI(m_ctrl.m_model.Score, m_ctrl.m_model.HighestScore);
        m_ctrl.m_cameraControl.ZoomIn();
        m_ctrl.m_gameManager.ResumeGame();
    }

    public override void DoBeforeLeaving()
    {
        m_ctrl.m_view.HideGameUI();
        m_ctrl.m_gameManager.PauseGame();
    }

    public void PauseButtonClicked()
    {
        m_ctrl.m_audioManager.PlayCursor();
        m_fsmState.PerformTransition(Transition.PauseButtonClick);
    }
}
