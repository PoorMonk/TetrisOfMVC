using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager m_instance = null;

    public static GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }   

    private bool m_isPause = true;
    public Shape[] m_shapes;
    public Color[] m_colors;
    [HideInInspector] public Ctrl m_ctrl;

    private Shape m_currentShape = null;
    private Transform m_blockHolder;

    private void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        m_ctrl = GameObject.Find("Ctrl").GetComponent<Ctrl>();
        m_blockHolder = GameObject.Find("BlockHolder").transform;
    }

    private void Update()
    {
        if (m_isPause) return;
        if(m_currentShape == null)
        {
            SpawnShape();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_currentShape.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_currentShape.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_currentShape.MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            m_currentShape.RotateSelf();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            m_currentShape.ResumeSpeed();
        }
    }

    public void StartGame()
    {
        m_isPause = false;
        if (m_currentShape != null)
        {
            Destroy(m_currentShape.gameObject);
            m_currentShape = null;
        }       
    }

    public void ResumeGame()
    {
        m_isPause = false;
        if (m_currentShape != null)
            m_currentShape.KeepFall();
    }

    public void PauseGame()
    {
        m_isPause = true;
        if (m_currentShape != null)
            m_currentShape.StopFall();
    }

    public void SpawnShape()
    {
        int iShapeIndex = Random.Range(0, m_shapes.Length);
        int iColorIndex = Random.Range(0, m_colors.Length);
        m_currentShape = Instantiate(m_shapes[iShapeIndex]);
        m_currentShape.Init(m_colors[iColorIndex]);
        m_currentShape.transform.SetParent(m_blockHolder);
    }

    private void ClearDestroyedBlock()
    {
        foreach (Transform t in m_blockHolder)
        {
            if (t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void ReSpawn()
    {
        ClearDestroyedBlock();
        if (m_ctrl.m_model.IsGameOver())
        {
            PauseGame();
            
            return;
        }
        m_currentShape = null;
    }
}
