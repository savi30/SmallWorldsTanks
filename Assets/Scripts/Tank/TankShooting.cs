using UnityEngine;
using UnityEngine.Networking;

public class TankShooting : NetworkBehaviour{
    public GameObject shell;
    public Transform fireTransform;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;

    private readonly string _fireButton = "Jump";
    private float _currentLaunchForce;
    private float _chargeSpeed;
    private bool _fired;

    private void OnEnable(){
        _currentLaunchForce = minLaunchForce;
    }

    private void Start(){
        _chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    private void Update(){
        if (PauseMenu.isOn)
            return;
        if (_currentLaunchForce >= maxLaunchForce && !_fired){
            _currentLaunchForce = maxLaunchForce;
            CmdFire();
        }
        else if (Input.GetButtonDown(_fireButton)){
            _fired = false;
            _currentLaunchForce = minLaunchForce;
        }
        else if (Input.GetButton(_fireButton) && !_fired){
            _currentLaunchForce += _chargeSpeed * Time.deltaTime;
        }
        else if (Input.GetButtonUp(_fireButton) && !_fired){
            CmdFire();
        }
    }

    [Command]
    private void CmdFire(){
        var shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = _currentLaunchForce * fireTransform.forward;
        NetworkServer.Spawn(shellInstance);

        _fired = true;
        _currentLaunchForce = minLaunchForce;
    }
}