using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour, IShieldForBuff
{
    public ChunkSpeed ChunkSpeed;
    public Car Car;

    [SerializeField] private Renderer[] _shieldBlinkRenderers;
    [SerializeField] private float _shieldFlashDuration = 0.08f;

    private bool _isShieldActive;
    private Coroutine _shieldFlashCoroutine;

    private void Awake()
    {
        if (_shieldBlinkRenderers == null || _shieldBlinkRenderers.Length == 0)
            _shieldBlinkRenderers = GetComponentsInChildren<Renderer>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (_isShieldActive)
            {
                CarHitAnimation hitAnimation = other.GetComponent<CarHitAnimation>();
                if (hitAnimation != null)
                    hitAnimation.PlayHitAnimation(transform);

                return;
            }

            CrushVolumeChanger.Instance.Crush();
            Car.Crush();
            other.GetComponent<CarHitAnimation>().PlayHitAnimation(transform);
            ChunkSpeed.ReduceSpeedAfterCrush();
        }
    }

    public void SetShieldActive(bool active)
    {
        _isShieldActive = active;

        if (!active)
            ResetShieldVisual();
    }

    public void FlashShield()
    {
        if (!isActiveAndEnabled)
            return;

        if (_shieldFlashCoroutine != null)
            StopCoroutine(_shieldFlashCoroutine);

        _shieldFlashCoroutine = StartCoroutine(FlashShieldRoutine());
    }

    public void ResetShieldVisual()
    {
        SetShieldRenderersVisible(true);
    }

    private IEnumerator FlashShieldRoutine()
    {
        SetShieldRenderersVisible(false);
        yield return new WaitForSeconds(_shieldFlashDuration);
        SetShieldRenderersVisible(true);

        _shieldFlashCoroutine = null;
    }

    private void SetShieldRenderersVisible(bool visible)
    {
        foreach (Renderer shieldBlinkRenderer in _shieldBlinkRenderers)
        {
            if (shieldBlinkRenderer != null)
                shieldBlinkRenderer.enabled = visible;
        }
    }
}
