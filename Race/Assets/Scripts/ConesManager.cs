using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ConesManager : MonoBehaviour
{
    [SerializeField] private float _jumpPower = 1.5f;
    [SerializeField] private float _jumpDuration = 0.5f;
    
    private ScoreManager _scoreManager;

    private Collider _coneCollider;
    private Collider _playerHitCollider;
    private bool _isHit;

    private void Awake()
    {
        _coneCollider = GetComponent<Collider>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _scoreManager = FindObjectOfType<ScoreManager>();

        _playerHitCollider = player.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!_coneCollider.bounds.Intersects(_playerHitCollider.bounds))
            return;

        HitCone();
    }

    private void HitCone()
    {
        if (_isHit)
            return;
        
        _isHit = true;
        _scoreManager.AddConesScore();
        _coneCollider.enabled = false;

        transform.SetParent(null);

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5);

        transform.DOJump(targetPos, _jumpPower,1, _jumpDuration).OnComplete(() => Destroy(gameObject));
    }
}