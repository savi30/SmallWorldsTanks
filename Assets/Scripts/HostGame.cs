using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour{
    [SerializeField] private uint roomSize = 4;
    private string _roomName;
    private NetworkManager _networkManager;

    private void Start(){
        _networkManager = NetworkManager.singleton;
        if (_networkManager.matchMaker == null){
            _networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name){
        _roomName = _name;
    }

    public void CreateRoom(){
        if (!string.IsNullOrEmpty(_roomName)){
            _networkManager.matchMaker.CreateMatch(_roomName, roomSize, true, "", "", "", 0, 0,
                _networkManager.OnMatchCreate);
        }
    }
}