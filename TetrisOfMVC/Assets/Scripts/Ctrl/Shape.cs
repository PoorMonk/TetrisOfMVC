using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{

    private bool m_isPause = false;
    private float m_timer = 0f;
    private float m_stepTime = 0.8f;
    private float m_multiTime = 15;
    private Transform m_rotatePoint;

    public void Init(Color col)
    {
        foreach (Transform t in transform)
        {
            if (t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = col;
            }
            else
            {
                m_rotatePoint = t.transform;
            }
        }
    }

    private void Update()
    {
        if (m_isPause) return;
        m_timer += Time.deltaTime;
        if (m_timer > m_stepTime)
        {
            m_timer = 0f;
            Fall();
        }
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;
        if (!GameManager.Instance.m_ctrl.m_model.IsValidMapPosition(transform))
        {
            pos.y += 1;
            transform.position = pos;
            StopFall();
            
            if (GameManager.Instance.m_ctrl.m_model.AddToMap(transform))
            {
                GameManager.Instance.m_ctrl.m_audioManager.PlayClearlineAudio();
            }
            GameManager.Instance.ReSpawn();
            return;
        }
        GameManager.Instance.m_ctrl.m_audioManager.PlayDrop();
    }

    public void StopFall()
    {
        m_isPause = true;
    }

    public void KeepFall()
    {
        m_isPause = false;
    }

    public void MoveLeft()
    {
        Vector3 pos = transform.position;
        pos.x -= 1;
        transform.position = pos;
        if (!GameManager.Instance.m_ctrl.m_model.IsValidMapPosition(transform))
        {
            pos.x += 1;
            transform.position = pos;            
        }
        GameManager.Instance.m_ctrl.m_audioManager.PlayMoveAudio();
    }

    public void MoveRight()
    {
        Vector3 pos = transform.position;
        pos.x += 1;
        transform.position = pos;
        if (!GameManager.Instance.m_ctrl.m_model.IsValidMapPosition(transform))
        {
            pos.x -= 1;
            transform.position = pos;
        }
        GameManager.Instance.m_ctrl.m_audioManager.PlayMoveAudio();
    }

    public void MoveDown()
    {
        m_stepTime /= m_multiTime;
    }

    public void ResumeSpeed()
    {
        m_stepTime = 0.8f;
    }

    public void RotateSelf()
    {
        transform.RotateAround(m_rotatePoint.position, Vector3.forward, -90);
        if (!GameManager.Instance.m_ctrl.m_model.IsValidMapPosition(transform))
        {
            transform.RotateAround(m_rotatePoint.position, Vector3.forward, 90);
        }
        else
        {
            GameManager.Instance.m_ctrl.m_audioManager.PlayMoveAudio();
        }
    }
}
