using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{
    public Rigidbody shell;
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
            Fire();
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
            Fire();
        }
    }

    void Fire()
    {
        fired = true;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;
        currentLaunchForce = minLaunchForce;
    }
}
