using UnityEngine;

public class WheelRotateModule
{
    private readonly ChunkController _chunkController;
    private readonly Transform[] _turnWheels;

    private float SteerAngle = 25f;
    private float SteerSpeed = 10f;

    public WheelRotateModule(ChunkController chunkController, Transform[] wheels)
    {
        _chunkController = chunkController;
        _turnWheels = wheels;
    }

    public void Tick()
    {
        float direction = -_chunkController.Direction;
        float targetAngle = direction * SteerAngle;

        foreach (var wheel in _turnWheels)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            wheel.localRotation = Quaternion.Lerp(wheel.localRotation, targetRotation, SteerSpeed * Time.deltaTime);
        }
    }
}