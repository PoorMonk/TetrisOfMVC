using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour {

    private Camera m_camera;

    private void Awake()
    {
        m_camera = Camera.main;
    }

    public void ZoomIn()
    {
        
    }

    public void ZoomOut()
    {
        m_camera.DOOrthoSize(20f, 0.5f);
    }
}
