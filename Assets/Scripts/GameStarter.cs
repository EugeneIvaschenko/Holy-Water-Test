using UnityEngine;

public class GameStarter : MonoBehaviour {
    [SerializeField] private Score score;
    [SerializeField] private CoinSpawner coinSpawner;
    [SerializeField] private PlayerController playerPrefab;

    private PlayerController player;

    private void Start() {
        player = Instantiate(playerPrefab);
        player.CoinCollected += score.AddPoint;
        coinSpawner.Init();
    }
}