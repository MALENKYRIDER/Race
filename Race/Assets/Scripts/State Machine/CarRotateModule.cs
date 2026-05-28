using UnityEngine;

public class CarRotateModule
{
    private readonly ChunkController _chunkController;
    private readonly Transform _car;

    private const float Angle = 15f;
    private const float Speed = 8f;

    public CarRotateModule(ChunkController chunkController, Transform car)
    {
        _chunkController = chunkController;
        _car = car;
    }

    public void Tick()
    {
        float direction = -_chunkController.Direction;
        Quaternion targetRotation = Quaternion.Euler(0f, direction * Angle, 0f);
        _car.transform.localRotation =
            Quaternion.Lerp(_car.transform.localRotation, targetRotation, Speed * Time.deltaTime);
    }
}