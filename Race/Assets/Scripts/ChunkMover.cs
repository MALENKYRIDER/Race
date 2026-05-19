using System;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    private ChunkSpawner _chunkSpawner;

    public void BlocksMover(float moveSpeed)
    {
        MoveBlocks(moveSpeed);
    }

    private void MoveBlocks(float moveSpeed)
    {
        _chunkSpawner = GetComponent<ChunkSpawner>();
        
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 moveOffSet = new Vector3(0, 0, -moveDistance);
        foreach (var activeChunk in _chunkSpawner.ActiveChunks)
        {
            activeChunk.transform.position += moveOffSet;
        }
    }
}