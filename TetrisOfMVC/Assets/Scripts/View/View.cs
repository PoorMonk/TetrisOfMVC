using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class View : MonoBehaviour {

    private RectTransform m_title;
    private RectTransform m_menuUI;

    private void Awake()
    {
        m_title = transform.Find("Canvas/TitleText") as RectTransform;
        m_menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
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

}
