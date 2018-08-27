using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip m_cursorClip;
    public AudioClip m_dropClip;
    public AudioClip m_moveClip;
    public AudioClip m_lineclearClip;
    private AudioSource m_audioSource;
    private bool m_isMute = false;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayCursor()
    {
        PlayAudio(m_cursorClip);
    }

    public void PlayDrop()
    {
        PlayAudio(m_dropClip);
    }

    public void PlayMoveAudio()
    {
        PlayAudio(m_moveClip);
    }

    public void PlayClearlineAudio()
    {
        PlayAudio(m_lineclearClip);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (m_isMute) return;
        m_audioSource.clip = clip;
        m_audioSource.Play();
    }

    public void SetMute(bool IsMute)
    {
        m_isMute = IsMute;
        if (!IsMute)
        {
            PlayCursor();
        }
    }
}
