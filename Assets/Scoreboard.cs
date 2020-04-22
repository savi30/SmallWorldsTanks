using UnityEngine;

public class Scoreboard : MonoBehaviour{
    [SerializeField] private GameObject playerScoreboardItemPrefab;
    [SerializeField] private Transform playerScoreboardParent;

    private void OnEnable(){
        TankManager[] tanks = GameManager.getAllPlayers();

        foreach (var tank in tanks){
            GameObject itemGO = Instantiate(playerScoreboardItemPrefab, playerScoreboardParent);
            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            if (item != null){
                item.Setup(tank.username, tank.deaths);
            }
        }
    }

    private void OnDisable(){
        foreach (Transform child in playerScoreboardParent){
            Destroy(child.gameObject);
        }
    }
}