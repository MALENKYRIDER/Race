using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanal;
    
    private bool _isPaused;
    private bool _canPause;

    private void Update()
    {
        if (!_canPause)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            TooglePause();
    }

    public void EnablePause()
    {
        _canPause = true;
    }
    
    public void DisablePause()
    {
        _canPause = false;
    }

    public void TooglePause()
    {
        if (_isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        _pausePanal.SetActive(true);
    }

    public void ResumeGame()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        _pausePanal.SetActive(false);
    }
}
