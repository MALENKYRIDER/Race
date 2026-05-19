using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateChunk : MonoBehaviour
{
    private ChunkSpawner _chunkSpawner;

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