using UnityEngine;

public class WheelCarModule
{
    private readonly ChunkManager _chunkManager;
    private readonly Transform[] _wheels;
    public ChunkSpeed _chunkSpeed;


    private bool _isWheelRotate;
    private float _currentSpeedModification;

    public WheelCarModule(ChunkManager chunkManager, Transform[] wheels, ChunkSpeed chunkSpeed)
    {
        _chunkSpeed = chunkSpeed;
        _chunkManager = chunkManager;
        _wheels = wheels;
    }

    public void Tick()
    {
        if (_isWheelRotate)
        {
            _currentSpeedModification += Time.deltaTime;
        }
        else
        {
            _currentSpeedModification -= Time.deltaTime;
        }

        _currentSpeedModification = Mathf.Clamp(_currentSpeedModification, 0f, 1f);
        float currentSpeed = _chunkSpeed.CurrentSpeed * _currentSpeedModification;
        foreach (var wheel in _wheels)
        {
            wheel.transform.Rotate(currentSpeed, 0, 0, Space.Self);
        }
    }

    public void StartWheel()
    {
        _isWheelRotate = true;
    }

    public void StopWheel()
    {
        _isWheelRotate = false;
    }
}