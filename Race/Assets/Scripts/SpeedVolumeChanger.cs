using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpeedVolumeChanger : MonoBehaviour
{
    public Volume volume;
    public ChunkSpeed chunkSpeed;
    public float maxEffectSpeed = 75f;
    public float maxLensIntensity = -0.35f;
    public float smoothSpeed = 6f;

    private LensDistortion _lensDistortion;

    public static SpeedVolumeChanger Instance;

    private void Awake()
    {
        Instance = this;

        if (volume != null)
        {
            volume.profile.TryGet(out _lensDistortion);
        }
    }

    private void Update()
    {
        if (_lensDistortion == null || chunkSpeed == null)
            return;

        float currentSpeed = chunkSpeed.CurrentSpeed * (1f + chunkSpeed._speedMultiplier);
        float effectPower = Mathf.InverseLerp(chunkSpeed.StartMoveSpeed, maxEffectSpeed, currentSpeed);
        float targetIntensity = Mathf.Lerp(0f, maxLensIntensity, effectPower);

        _lensDistortion.intensity.value = Mathf.Lerp(
            _lensDistortion.intensity.value,
            targetIntensity,
            smoothSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        if (_lensDistortion != null)
            _lensDistortion.intensity.value = 0f;
    }
}
