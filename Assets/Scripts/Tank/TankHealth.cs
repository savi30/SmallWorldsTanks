using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankHealth : NetworkBehaviour
{
    [Client]
    public void ApplyDamage(float damage)
    {
        Debug.Log("Damage applied");
        CmdApplyDamage();
    }

    [Command]
    public void CmdApplyDamage()
    {
        Debug.Log("Damage applied on server");
    }

}
