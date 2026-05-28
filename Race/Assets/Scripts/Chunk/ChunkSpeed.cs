using UnityEngine;

public class ChunkSpeed : MonoBehaviour, ISpeedForBuff, IMultiplierSpeedForBuff
{
    public float StartMoveSpeed = 10f;
    public float MaxSpeed = 30f;
    public float SpeedIncreasePerSecond = 0.4f;

    private float _currentSpeed = 0f;
    public float CurrentSpeed => _currentSpeed;
    
    public float _speedMultiplier;

    private void RecalculateSpeed()
    {
        if (_currentSpeed < StartMoveSpeed)
        {
            _currentSpeed += StartMoveSpeed * Time.deltaTime;
            if (_currentSpeed > StartMoveSpeed)
            {
                _currentSpeed = StartMoveSpeed;
            }
        }
        else
        {
            if (_currentSpeed != MaxSpeed)
            {
                _currentSpeed += SpeedIncreasePerSecond * Time.deltaTime;
            }
        }

        if (_currentSpeed > MaxSpeed)
            _currentSpeed = MaxSpeed;
    }

    public void SpeedRecalculation()
    {
        RecalculateSpeed();
    }
    
    public void ReduceSpeedAfterCrush()
    {
        _currentSpeed *= 0.5f;
        if (_currentSpeed < StartMoveSpeed)
        {
            Debug.Log("Lose");
        }
    }

    public void AddSpeed(float speed)
    {
        _currentSpeed += speed;
    }

    public void RemoveSpeed(float speed)
    {
        _currentSpeed -= speed;
        if (_currentSpeed < StartMoveSpeed) 
            _currentSpeed = StartMoveSpeed;
    }

    public void AddSpeedMultiplier(float speedMultiplier)
    {
        this._speedMultiplier += speedMultiplier;
    }

    public void RemoveSpeedMultiplier(float speedMultiplier)
    {
        this._speedMultiplier -= speedMultiplier;
    }
}
