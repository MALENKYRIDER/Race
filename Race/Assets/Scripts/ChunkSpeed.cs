using UnityEngine;

public class ChunkSpeed : MonoBehaviour
{
    public float StartMoveSpeed = 10f;
    public float MaxSpeed = 30f;
    public float SpeedIncreasePerSecond = 0.4f;

    private float _currentSpeed = 0f;
    public float CurrentSpeed => _currentSpeed;

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
}