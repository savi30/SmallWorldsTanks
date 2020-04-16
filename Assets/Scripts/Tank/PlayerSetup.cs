using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(TankManager))]
public class PlayerSetup : NetworkBehaviour{
    private const string PayerTag = "Player";
    [SerializeField] private GameObject playerUIPrefab;
    [SerializeField] private Behaviour[] componentsToDisable;

    private GameObject _playerUiInstance;
    private Camera _sceneCamera;
    private string _netId;

    private void Start(){
        if (!isLocalPlayer){
            foreach (var element in componentsToDisable){
                element.enabled = false;
                gameObject.tag = PayerTag;
            }
        }
        else{
            _sceneCamera = Camera.main;
            if (_sceneCamera != null){
                Camera.main.gameObject.SetActive(false);
            }

            _playerUiInstance = Instantiate(playerUIPrefab);
            _playerUiInstance.name = playerUIPrefab.name;

            PlayerUI ui = _playerUiInstance.GetComponent<PlayerUI>();
            GetComponent<TankManager>().SetUI(ui);
        }
    }

    public override void OnStartClient(){
        base.OnStartClient();
        _netId = GetComponent<NetworkIdentity>().netId.ToString();
        var tankManager = GetComponent<TankManager>();
        GameManager.RegisterPlayer(_netId, tankManager);
        Debug.Log("registering " + _netId);
        tankManager.instance.transform.name = _netId.ToString();
    }

    public void OnDisable(){
        Destroy(_playerUiInstance);
        if (_sceneCamera != null){
            _sceneCamera.gameObject.SetActive(true);
        }

        GameManager.DeRegisterPlayer(_netId);
    }
}