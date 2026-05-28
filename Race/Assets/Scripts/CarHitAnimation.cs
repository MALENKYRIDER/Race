using System.Xml.Serialization;
using DG.Tweening;
using UnityEngine;

public class CarHitAnimation : MonoBehaviour
{
    [SerializeField] private float _jumpPower = 1.4f;
    [SerializeField] private float _jumpDuration = 0.55f;
    [SerializeField] private float _scatterBack = 4.5f;
    [SerializeField] private float _scatterSide = 2.5f;
    private bool _hasHit;
    public bool CanBeHit => !_hasHit;

    public void PlayHitAnimation(Transform player)
    {
        if (_hasHit) return;
        _hasHit = true;

        transform.SetParent(null);
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        transform.DOKill();

        float side = Mathf.Sign(transform.position.x - player.position.x);
        if (Mathf.Approximately(side, 0f))
            side = Random.value > 0.5f ? 1f : -1f;
        Vector3 knockSide = player.right * side;

        transform.position += knockSide * 0.45f + Vector3.up * 0.05f;
        Vector3 landing = player.position
                          - player.forward * _scatterBack
                          + knockSide * _scatterSide;
        landing.y = transform.position.y;
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOJump(landing, _jumpPower, 1, _jumpDuration)
            .SetEase(Ease.OutQuad));
        seq.Join(transform.DORotate(
            new Vector3(
                Random.Range(-20f, 20f),
                Random.Range(-40f, 40f),
                side * Random.Range(60f, 100f)),
            _jumpDuration,
            RotateMode.FastBeyond360));
        if (Camera.main != null)
            seq.Join(Camera.main.transform.DOShakePosition(0.22f, 0.25f, 10, 40f));

        seq.OnComplete(() => Destroy(gameObject));
    }
    // public void PlayHitAnimation(Transform referenceObject)
    // {
    //    transform.parent = null;
    //    Sequence mySequence = DOTween.Sequence();
    //    Vector3 addPosition = Random.Range(0, 10) > 5 ? Vector3.right : Vector3.left;
    //    mySequence.Append(transform.DOJump(referenceObject.transform.position - (referenceObject.transform.forward + addPosition) * 6, 3,1,0.4f));
    //    mySequence.Join(transform.DOLocalRotate(new Vector3(Random.Range(-24, 24), Random.Range(-45, 45), 100), 0.2f));
    //    mySequence.Join(Camera.main.transform.DOShakePosition(0.2f, 0.3f)).OnComplete(() => Destroy(gameObject));
    // }
}