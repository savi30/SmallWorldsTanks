using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour{
    public static bool isOn = false;

    public void LeaveRoom(){
        NetworkManager manager = NetworkManager.singleton;
        MatchInfo matchInfo = manager.matchInfo;
        manager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, manager.OnDropConnection);
        manager.StopHost();
    }
}