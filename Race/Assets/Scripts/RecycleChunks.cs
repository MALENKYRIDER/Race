using UnityEngine;

public class RecycleChunks : MonoBehaviour
{
    private InstantiateChunk _instantiateChunk;
    private ChunkSpawner _chunkSpawner; 
    
    public float RecycleDistanceBehindCamera = 15f;

    public void RecycleBlocksPasasedCamera()
    {
        RecycleBlockPasasedCamera();
    }

    private void RecycleBlockPasasedCamera()
    {
        _instantiateChunk = GetComponent<InstantiateChunk>();
        _chunkSpawner = GetComponent<ChunkSpawner>();
        
        float recycleThreshold = _chunkSpawner.CameraTransform.position.z - RecycleDistanceBehindCamera;

        while (_chunkSpawner.ActiveChunks.Count > 0)
        {
            Transform oldestBlock = _chunkSpawner.ActiveChunks[0];
            if (oldestBlock.position.z >= recycleThreshold)
                return;

            Transform recycleChunk = _chunkSpawner.ActiveChunks[0];
            _chunkSpawner.ActiveChunks.Remove(recycleChunk);
            float frontBlockZPosition = _chunkSpawner.ActiveChunks.Count == 0 ? recycleChunk.position.z : GetFrontPositionZ();
            float nextBlockZPosition = frontBlockZPosition + _chunkSpawner.BlockLength;
            Destroy(oldestBlock.gameObject);
            Transform newBlock = _instantiateChunk.ChunksInstantiate(nextBlockZPosition);
            _chunkSpawner.ActiveChunks.Add(newBlock);
        }
    }

    private float GetFrontPositionZ()
    {
        float returnValue = float.MinValue;
        foreach (var activeChunk in _chunkSpawner.ActiveChunks)
        {
            if (activeChunk.position.z > returnValue)
                returnValue = activeChunk.position.z;
        }
    
        return returnValue;
    }
}