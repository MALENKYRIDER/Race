using DG.Tweening;
using UnityEngine;

public class CoinFlySpawner : MonoBehaviour
{
    public ChunkManager chunkManager;
    public Transform[] SpawnCoinPosition;
    public GameObject CoinPrefab;
    public float SpawnInterval;

    private bool _canSpawn = true;
    
    private int _spawnLineId;
    private int _coinInLine;
    
    public void SpawnCoin()
    {
        if (_canSpawn == false) return;

        if (_coinInLine == 0)
        {
            _spawnLineId = Random.Range(0, SpawnCoinPosition.Length);
            _coinInLine = Random.Range(4, 10);
        }
        
        _canSpawn = false;
        DOVirtual.DelayedCall(SpawnInterval, () => { _canSpawn = true; });
        
        GameObject newCoin = Instantiate(CoinPrefab, SpawnCoinPosition[_spawnLineId].position, Quaternion.identity);
        Transform lastChunk = chunkManager.GetLastChunk();
        newCoin.transform.parent = lastChunk.transform;
        Destroy(newCoin.gameObject, 5);
        _coinInLine--;
    }
}