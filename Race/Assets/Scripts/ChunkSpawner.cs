using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    private InstantiateChunk _instantiateChunk;
    private List<Transform> _activeChunks = new List<Transform>();
    private List<GameObject> _lastChunk = new List<GameObject>();
    private List<GameObject> _chunks = new List<GameObject>();

    public Transform CameraTransform;
    public List<GameObject> Chunks = new List<GameObject>();
    
    public List<Transform> ActiveChunks => _activeChunks;
    public List<GameObject> LastChunk => _lastChunk;

    public int InitialBlockCount = 8;
    public float BlockLength = 10f;

    public void InitialChunksSpawn()
    {
        SpawnInitialChunks();
    }

    public GameObject GetChunks()
    {
        return GetChunk();
    }

    private void SpawnInitialChunks()
    {
        _instantiateChunk = GetComponent<InstantiateChunk>();

        float nextSpawnPositionZ = CameraTransform.position.z;
        for (int i = 0; i < InitialBlockCount; i++)
        {
            Transform spawnedChunk = _instantiateChunk.ChunksInstantiate(nextSpawnPositionZ);
            _activeChunks.Add(spawnedChunk);
            nextSpawnPositionZ += BlockLength;
        }
    }

    private GameObject GetChunk()
    {
        if (_lastChunk.Count >= 2)
        {
            var randomChunk = Chunks[Random.Range(0, Chunks.Count)];
            while (_lastChunk.Contains(randomChunk))
            {
                randomChunk = Chunks[Random.Range(0, Chunks.Count)];
            }

            return randomChunk;
        }

        return Chunks[Random.Range(0, Chunks.Count)];
    }
}