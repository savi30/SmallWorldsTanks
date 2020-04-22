using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour{
    private NetworkManager _networkManager;
    private List<GameObject> rooms = new List<GameObject>();
    [SerializeField] private Text status;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform roomListParent;

    // Start is called before the first frame update
    void Start(){
        _networkManager = NetworkManager.singleton;
        if (_networkManager.matchMaker == null){
            _networkManager.StartMatchMaker();
        }

        RefreshRooms();
    }

    public void RefreshRooms(){
        rooms.Clear();
        _networkManager.matchMaker.ListMatches(0, 5, "", false, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string info, List<MatchInfoSnapshot> matches){
        status.text = "";
        if (matches == null){
            status.text = "Network Error";
            return;
        }

        if (matches.Count == 0){
            status.text = "No rooms found";
        }

        ClearRoomList();
        foreach (var match in matches){
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab, roomListParent);

            RoomListItem roomListItem = _roomListItemGO.GetComponent<RoomListItem>();
            if (roomListItem != null){
                roomListItem.Setup(match, JoinRoom);
            }

            rooms.Add(_roomListItemGO);
        }
    }

    private void ClearRoomList(){
        for (int i = 0; i < rooms.Count; i++){
            Destroy(rooms[i]);
        }

        rooms.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot snapshot){
        _networkManager.matchMaker.JoinMatch(snapshot.networkId, "", "", "", 0, 0, _networkManager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining...";
    }
}