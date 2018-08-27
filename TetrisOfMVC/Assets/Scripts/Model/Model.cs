using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

    private const int m_normalRows = 20;
    private const int m_maxCols = 10;
    private const int m_maxRows = 23;

    public int m_score = 0;
    public int m_highestScore = 0;
    public int m_gameCount = 0;

    private Transform[,] m_map = new Transform[m_maxCols, m_maxRows];

    public int Score
    {
        get
        {
            return m_score;
        }
    }

    public int GameCount
    {
        get
        {
            return m_gameCount;
        }
    }

    public int HighestScore
    {
        get
        {
            return m_highestScore;
        }
    }

    private void Awake()
    {
        LoadData();
    }

    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            if (IsInMap(pos) == false) return false; 
            if (m_map[(int)pos.x, (int)pos.y] != null) return false;
        }
        return true;
    }

    public bool AddToMap(Transform t)
    {
        foreach(Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            m_map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }

    private bool CheckMap()
    {
        int iCount = 0;
        for (int i = 0; i < m_maxRows; ++i)
        {
            bool isFull = IsFull(i);
            if (isFull)
            {
                iCount++;
                RemoveFullRow(i);
                MoveDownRowAbove(i + 1);
                --i;
            }
        }
        if (iCount > 0)
        {
            m_score += iCount * 100;
            if (m_score > m_highestScore)
                m_highestScore = m_score;
            GameManager.Instance.m_ctrl.m_view.UpdataGameUI(m_score, m_highestScore);
            return true;
        }            
        else
            return false;
    }

    private bool IsFull(int row)
    {
        for (int i = 0; i < m_maxCols; ++i)
        {
            if (m_map[i, row] == null)
                return false;
        }
        return true;
    }

    private void RemoveFullRow(int row)
    {
        for (int i = 0; i < m_maxCols; ++i)
        {
            Destroy(m_map[i, row].gameObject);
            m_map[i, row] = null;
            //Debug.Log("row:" + row + ", col:" + i);
        }
    }

    private void MoveDownRowAbove(int row)
    {
        for (int i = row; i < m_maxRows; ++i)
        {
            MoveRowDown(i);
        }
    }

    public bool IsGameOver()
    {
        for (int i = m_normalRows; i < m_maxRows; ++i)
        {
            for (int j = 0; j < m_maxCols; ++j)
            {
                if (m_map[j, i] != null)
                {
                    GameManager.Instance.m_ctrl.m_view.ShowGameOverUI(m_score);
                    m_gameCount++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }

    private void MoveRowDown(int row)
    {
        for (int i = 0; i < m_maxCols; ++i)
        {
            if (m_map[i, row] != null)
            {
                m_map[i, row - 1] = m_map[i, row];
                m_map[i, row] = null;
                m_map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    private bool IsInMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < m_maxCols && pos.y >= 0;
    }

    public void LoadData()
    {
        m_gameCount = PlayerPrefs.GetInt("gameCount", 0);
        m_highestScore = PlayerPrefs.GetInt("highestScore", 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("gameCount", m_gameCount);
        PlayerPrefs.SetInt("highestScore", m_highestScore);
    }

    public void Restart()
    {
        for (int i = 0; i < m_maxCols; ++i)
        {
            for (int j = 0; j < m_maxRows; ++j)
            {
                if (m_map[i, j] != null)
                {
                    Destroy(m_map[i, j].gameObject);
                    m_map[i, j] = null;
                }
            }
        }

        m_score = 0;
    }

    public void ClearData()
    {
        m_score = 0;
        m_highestScore = 0;
        m_gameCount = 0;
        SaveData();
    }
}
