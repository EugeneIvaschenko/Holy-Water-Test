using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour {
    [SerializeField] private BoxCollider floor;
    [SerializeField] private Coin coinPrefab;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float coinLifetime = 5;
    [SerializeField] private float spawnHeight = 5;

    public void Init() {
        StartCoroutine(StartCoinCreatingCycle());
    }

    private IEnumerator StartCoinCreatingCycle() {
        while (true) {
            yield return new WaitForSeconds(spawnDelay);
            CreateNewCoin();
        }
    }

    private void CreateNewCoin() {
        float x = Random.Range(floor.bounds.min.x, floor.bounds.max.x);
        float z = Random.Range(floor.bounds.min.z, floor.bounds.max.z);
        float y = floor.bounds.max.y + spawnHeight;

        Coin coin = Instantiate(coinPrefab);
        coin.transform.position = new Vector3(x, y, z);
        coin.SetLifetime(coinLifetime);
    }
}