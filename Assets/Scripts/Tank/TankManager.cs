using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class TankManager : NetworkBehaviour{
    [SyncVar] private bool _isDead;
    private TankMovement _movement;
    private TankShooting _shooting;
    private PlayerUI _playerUi;

    [SyncVar] public float currentHealth;
    public float maxHealth = 100f;
    public GameObject instance;
    public Material[] materials;

    public bool isDead{
        get{ return _isDead; }
        protected set{ _isDead = value; }
    }

    private void Awake(){
        _movement = gameObject.GetComponent<TankMovement>();
        _shooting = gameObject.GetComponent<TankShooting>();
        instance = gameObject;
        Setup();
    }

    private void Setup(){
        isDead = false;
        currentHealth = maxHealth;
        AssignMaterial();
    }

    private void AssignMaterial(){
        var random = new Random();
        var value = random.Next(0, 4);
        var playerMaterial = materials[value];
        var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var rend in renderers){
            rend.material = playerMaterial;
        }
    }

    public void ApplyDamage(float damage){
        if (!isServer) return;
        if (isDead) return;
        currentHealth -= damage;
        Rpc_ApplyDamage();
    }

    [ClientRpc]
    private void Rpc_ApplyDamage(){
        if (currentHealth <= 0){
            Die();
        }

        UpdateUI();
    }

    private void UpdateUI(){
        if (isLocalPlayer)
            _playerUi.SetFill(currentHealth / 100);
    }

    private void Die(){
        isDead = true;
        DisableControl();
    }

    private void DisableControl(){
        if (!isLocalPlayer) return;
        _movement.enabled = false;
        _shooting.enabled = false;
    }

    public void EnableControl(){
        _movement.enabled = true;
        _shooting.enabled = true;
    }

    public void SetUI(PlayerUI ui){
        _playerUi = ui;
    }
}