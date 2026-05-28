using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateChunk : MonoBehaviour
{
    private ChunkSpawner _chunkSpawner;
    
    private int amountSpawnedChunk;

    public Transform ChunksInstantiate(float zPosition)
    {
        return InstantiateChunks(zPosition);
    }
    
    private Transform InstantiateChunks(float zPosition)
    {
        _chunkSpawner = GetComponent<ChunkSpawner>();
        
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, zPosition);
        var lastChunk = _chunkSpawner.GetChunks();
        GameObject newChunk = Instantiate(lastChunk);

        amountSpawnedChunk++;
        switch (amountSpawnedChunk)
        {
            case < 10 : newChunk.GetComponent<Chunk>().ChunkSpawned(Random.Range(0,1)); break;
            case < 50 : newChunk.GetComponent<Chunk>().ChunkSpawned(Random.Range(0,2)); break;
            case < 70 : newChunk.GetComponent<Chunk>().ChunkSpawned(2); break;
        }
        
        if (_chunkSpawner.LastChunk.Count >= 2)
        {
            _chunkSpawner.LastChunk.Remove(_chunkSpawner.LastChunk.First());
        }

        _chunkSpawner.LastChunk.Add(lastChunk);
        newChunk.transform.position = spawnPosition;
        newChunk.transform.SetParent(transform);
        return newChunk.transform;
    }
}