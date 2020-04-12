using UnityEngine;
using UnityEngine.Networking;
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }else
        {
            Camera.main.gameObject.SetActive(false);
        }

        RegisterPlayer();
    }

    void RegisterPlayer()
    {
        string ID = "Player" + GetComponent<NetworkIdentity>().netId.ToString();
        transform.name = ID;
    }

}
