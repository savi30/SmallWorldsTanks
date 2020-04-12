using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material[] playerMaterials;

    private static Dictionary<string, TankManager> tanks = new Dictionary<string, TankManager>();

    public static void registerPlayer(string netID, TankManager tankManager)
    {
        string playerID;
    }
}
