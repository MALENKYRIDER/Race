using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private InstantiateChunk _instantiateChunks;
    [SerializeField] private ChunkSpawner _chunksSpawner;
    [SerializeField] private ChunkSpeed _chunksSpeed;
    [SerializeField] private ChunkMover _chunksMover;
    [SerializeField] private RecycleChunks _recycleChunk;
    
    private InstantiateChunk _instantiateChunk;
    private ChunkSpawner _chunkSpawner; 
    private ChunkSpeed _chunkSpeed; 
    private ChunkMover _chunkMover;
    private RecycleChunks _recycleChunks;
    
    
    
    private void Awake()
    {
        _chunkSpawner = GetComponent<ChunkSpawner>();
        _chunkSpeed = GetComponent<ChunkSpeed>();
        _chunkMover = GetComponent<ChunkMover>();
        _recycleChunks = GetComponent<RecycleChunks>();
        _instantiateChunk = GetComponent<InstantiateChunk>();
        
        _chunkSpawner.InitialChunksSpawn();
    }

    private void Update()
    {
        _chunkSpeed.SpeedRecalculation();
        _chunkMover.BlocksMover(_chunkSpeed.CurrentSpeed);
        _recycleChunks.RecycleBlocksPasasedCamera();
    }
}