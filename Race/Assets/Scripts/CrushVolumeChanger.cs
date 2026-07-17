using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CrushVolumeChanger : MonoBehaviour
{
    public Volume volume;
    private Vignette _vignette;

    public static CrushVolumeChanger Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Crush()
    {
        if (volume.profile.TryGet(out _vignette))
        {
            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0.8f, 0.3f)
                .SetLoops(2, LoopType.Yoyo).SetEase(Ease.Linear).OnComplete(() => { _vignette.intensity.value = 0; });
        }
    }
}