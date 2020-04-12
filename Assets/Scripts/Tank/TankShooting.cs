using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TankShooting : NetworkBehaviour
{
    public GameObject shell;
    public Transform fireTransform;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;

    private string fireButton = "Jump";
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;

    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
    }

    void Start()
    {
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    void Update()
    {
        if (currentLaunchForce >= maxLaunchForce && !fired)
        {
            currentLaunchForce = maxLaunchForce;
            Cmd_Fire();
        }
        else if (Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
        }
        else if (Input.GetButton(fireButton) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;
        }
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            Cmd_Fire();
        }
    }

    [Command]
    void Cmd_Fire()
    {
        fired = true;
        GameObject shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = currentLaunchForce * fireTransform.forward;
        NetworkServer.Spawn(shellInstance);
        currentLaunchForce = minLaunchForce;
    }
}
