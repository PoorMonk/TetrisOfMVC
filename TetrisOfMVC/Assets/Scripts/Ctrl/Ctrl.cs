using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour {

    [HideInInspector] public Model m_model;
    [HideInInspector] public View m_view;
    [HideInInspector] public CameraControl m_cameraControl;
    [HideInInspector] public GameManager m_gameManager;
    [HideInInspector] public AudioManager m_audioManager;

    private FSMSystem m_fsm;

    private void Awake()
    {
        m_model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        m_view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        m_cameraControl = GetComponent<CameraControl>();
        m_audioManager = GetComponent<AudioManager>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        MakeFSM();
    }

    void MakeFSM()
    {
        m_fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states)
        {
            m_fsm.AddState(state, this);
        }
        MenuState s = GetComponentInChildren<MenuState>();
        m_fsm.SetCurrentState(s);
    }

}
