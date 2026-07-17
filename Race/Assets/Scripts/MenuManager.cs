using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private const string MusicVolumeKey = "Music_Volume";
    private const string SfxVolumeKey = "SFX_Volume";

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Button _race;
    [SerializeField] private GameObject _menu;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _scoreUI;
    
    [SerializeField] private PauseManager _pauseManager;

    private void Awake()
    {
        Time.timeScale = 0f;

        _musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, 0.7f);
        _sfxSlider.value = PlayerPrefs.GetFloat(SfxVolumeKey, 0.7f);

        ApplyMusicVolume(_musicSlider.value);
        ApplySfxVolume(_sfxSlider.value);
    }

    private void OnEnable()
    {
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        _race.onClick.AddListener(OnRaceButtonClick);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        _sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        _race.onClick.RemoveListener(OnRaceButtonClick);
    }

    private void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, value);
        ApplyMusicVolume(value);
    }

    private void OnSfxVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
        ApplySfxVolume(value);
    }

    private void OnRaceButtonClick()
    {
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        _menu.SetActive(false);
        _scoreUI.SetActive(true);
        _pauseManager.EnablePause();
    }

    private void ApplyMusicVolume(float value)
    {
        _audioMixer.SetFloat("Music_Volume", SliderValueToDecibels(value));
    }

    private void ApplySfxVolume(float value)
    {
        _audioMixer.SetFloat("SFX_Volume", SliderValueToDecibels(value));
    }

    private float SliderValueToDecibels(float value)
    {
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
    }
}