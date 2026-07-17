using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _distanceScoreText;
    [SerializeField] private Text _hittedConesText;

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Text _finalDistanceScoreText;
    [SerializeField] private Text _finalHittedConesText;
    [SerializeField] private Text _finalTotalScoreText;

    [SerializeField] private float _distanceScorePerSecond = 10f;
    [SerializeField] private float _hittedConesTime = 0.5f;
    
    [SerializeField] private PauseManager _pauseManager;

    private float _distanceScore;
    private int _conesScore;
    private bool _isGameActive = true;
    private Coroutine _hittedConesCoroutine;

    private void Update()
    {
        if (!_isGameActive)
            return;
        
        _distanceScore += _distanceScorePerSecond * Time.deltaTime;
        _distanceScoreText.text = "" + Mathf.FloorToInt(_distanceScore);
    }

    public void AddConesScore()
    {
        if (!_isGameActive)
            return;

        _conesScore++;
        _hittedConesText.text = "+" + _conesScore;
        _hittedConesText.gameObject.SetActive(true);

        if (_hittedConesCoroutine != null)
            StopCoroutine(_hittedConesCoroutine);

        _hittedConesCoroutine = StartCoroutine(HideHittedConesAfterTime());
    }

    public void ShowLosePanel()
    {
        _isGameActive = false;
        Time.timeScale = 0;

        int finalDistanceScore = Mathf.FloorToInt(_distanceScore);
        int finalTotalScore = finalDistanceScore + _conesScore;

        _losePanel.SetActive(true);
        _finalDistanceScoreText.text = "Distance score: " + finalDistanceScore;
        _finalHittedConesText.text = "Hitted cones: " + _conesScore;
        _finalTotalScoreText.text = "Total score: " + finalTotalScore;
        
        _pauseManager.DisablePause();
    }

    private IEnumerator HideHittedConesAfterTime()
    {
        yield return new WaitForSeconds(_hittedConesTime);
        _hittedConesText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}