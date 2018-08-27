using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class View : MonoBehaviour {

    private RectTransform m_title;
    private RectTransform m_menuUI;
    private RectTransform m_gameUI;
    private GameObject m_restartButton;
    private GameObject m_gameOverUI;
    private GameObject m_settingUI;
    private GameObject m_rankUI;

    private Text m_scoreText;
    private Text m_highestScoreText;
    private Text m_gameOverScoreText;

    private Text m_rankScoreText;
    private Text m_rankHighestScoreText;
    private Text m_rankGameCountText;

    private bool m_isMute = false;
    private GameObject m_muteGameObject;

    private void Awake()
    {
        m_title = transform.Find("Canvas/TitleText") as RectTransform;
        m_menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        m_gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        m_gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        m_settingUI = transform.Find("Canvas/SettingUI").gameObject;
        m_rankUI = transform.Find("Canvas/RankUI").gameObject;
        m_restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;

        m_scoreText = GameObject.Find("Canvas/GameUI/ScoreText/Text").GetComponent<Text>();
        m_highestScoreText = GameObject.Find("Canvas/GameUI/HighestScoreText/Text").GetComponent<Text>();
        m_gameOverScoreText = transform.Find("Canvas/GameOverUI/Image/ScoreText").GetComponent<Text>();

        
    }

    public void ShowMenu()
    {
        m_title.gameObject.SetActive(true);
        m_title.DOAnchorPosY(-95f, 0.5f);
        m_menuUI.gameObject.SetActive(true);
        m_menuUI.DOAnchorPosY(9f, 0.5f);
    }

    public void HideMenu()
    {
        m_title.DOAnchorPosY(98f, 0.5f).OnComplete(delegate { m_title.gameObject.SetActive(false); });
        m_menuUI.DOAnchorPosY(-109f, 0.5f).OnComplete(delegate { m_menuUI.gameObject.SetActive(false); });
    }

    public void UpdataGameUI(int score, int highestScore)
    {
        m_scoreText.text = score.ToString();
        m_highestScoreText.text = highestScore.ToString();
    }

    public void ShowGameUI(int score = 0, int highestScore = 0)
    {
        m_gameUI.gameObject.SetActive(true);
        m_gameUI.DOAnchorPosY(-72.5f, 0.5f);
        UpdataGameUI(score, highestScore);
    }

    public void HideGameUI()
    {        
        m_gameUI.DOAnchorPosY(75.1f, 0.5f).OnComplete(delegate { m_gameUI.gameObject.SetActive(false); });
        m_restartButton.SetActive(true);
    }

    public void ShowGameOverUI(int score = 0)
    {
        m_gameOverUI.SetActive(true);
        m_gameOverScoreText.text = score.ToString();
    }

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnRestartButtonClick()
    {
        m_gameOverUI.SetActive(false);
        GameManager.Instance.m_ctrl.m_model.Restart();
        GameManager.Instance.StartGame();
        UpdataGameUI(0, GameManager.Instance.m_ctrl.m_model.HighestScore);       
    }

    public void OnSettingButtonClick()
    {
        m_settingUI.SetActive(true);
        m_muteGameObject = GameObject.Find("Canvas/SettingUI/Image/AudioButton/MuteImage").gameObject;
    }

    public void OnHideSettingUI()
    {
        m_settingUI.SetActive(false);
    }

    public void OnSetMuteClick()
    {
        m_isMute = !m_isMute;
        GameManager.Instance.m_ctrl.m_audioManager.SetMute(m_isMute);
        m_muteGameObject.SetActive(m_isMute);
    }
    private void OnInitRankInfo()
    {
        m_rankScoreText = GameObject.Find("Canvas/RankUI/Image/CurrentScoreText/Text").GetComponent<Text>();
        m_rankHighestScoreText = GameObject.Find("Canvas/RankUI/Image/HighestScoreText/Text").GetComponent<Text>();
        m_rankGameCountText = GameObject.Find("Canvas/RankUI/Image/GameCountText/Text").GetComponent<Text>();
    }

    private void UpdateRankInfo()
    {
        m_rankScoreText.text = GameManager.Instance.m_ctrl.m_model.Score.ToString();
        m_rankHighestScoreText.text = GameManager.Instance.m_ctrl.m_model.HighestScore.ToString();
        m_rankGameCountText.text = GameManager.Instance.m_ctrl.m_model.GameCount.ToString();
    }

    public void OnShowRankUI()
    { 
        m_rankUI.SetActive(true);
        OnInitRankInfo();
        UpdateRankInfo();
    }

    public void OnHideRankUI()
    {
        m_rankUI.SetActive(false);
    }

    public void OnDeleteButtonClick()
    {
        GameManager.Instance.m_ctrl.m_model.ClearData();
        UpdateRankInfo();
    }
}
