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
        return InstantiateChunks(zPosition, null, true);
    }

    public Transform ChunksInstantiate(float zPosition, GameObject chunkPrefab, bool spawnObjects)
    {
        return InstantiateChunks(zPosition, chunkPrefab, spawnObjects);
    }
    
    private Transform InstantiateChunks(float zPosition, GameObject chunkPrefab, bool spawnObjects)
    {
        _chunkSpawner = GetComponent<ChunkSpawner>();
        
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, zPosition);
        var lastChunk = chunkPrefab != null ? chunkPrefab : _chunkSpawner.GetChunks();
        GameObject newChunk = Instantiate(lastChunk);

        if (spawnObjects)
        {
            amountSpawnedChunk++;
            int amountObjects = amountSpawnedChunk switch
            {
                < 2 => 0,
                < 10 => 1,
                < 50 => Random.Range(1, 3),
                _ => 3
            };
            
            var chunks = newChunk.GetComponentsInChildren<Chunk>();
            foreach (var chunk in chunks)
            {
                chunk.ChunkSpawned(amountObjects);
            }
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
