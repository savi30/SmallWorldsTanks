using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager instance;

    private void Awake(){
        if (instance != null){
            Debug.LogError("More than one manager in the scene");
        }
        else{
            instance = this;
        }
    }

    public MatchSettings matchSettings;

    public static TankManager[] getAllPlayers(){
        return _tanks.Values.ToArray();
    }

    #region PlayerTracking

    private static Dictionary<string, TankManager> _tanks = new Dictionary<string, TankManager>();

    public static void RegisterPlayer(string netID, TankManager tankManager){
        _tanks.Add(netID, tankManager);
    }

    public static void DeRegisterPlayer(string netId){
        _tanks.Remove(netId);
    }

    public static TankManager GetPlayer(string netId){
        return _tanks[netId];
    }

    #endregion
}