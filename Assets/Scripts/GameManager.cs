using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
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
}