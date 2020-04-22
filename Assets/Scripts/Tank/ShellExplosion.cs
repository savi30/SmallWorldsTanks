using UnityEngine;
using UnityEngine.Networking;

public class ShellExplosion : NetworkBehaviour{
    private const string PlayerTag = "Player";

    public GameObject explosionParticles;
    public float maxDamage = 50f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;

    private void Start(){
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision other){
        var colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var col in colliders){
            if (!col.CompareTag(PlayerTag)) continue;
            Debug.Log(col.gameObject.name);
            var target = col.GetComponent<Rigidbody>();
            if (!target)
                continue;
            target.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            var tankManager = target.GetComponent<TankManager>();
            if (!tankManager)
                continue;
            DealDamage(target.position, tankManager);
        }

        Cmd_PlayExplosion();
        Destroy(gameObject);
    }

    private void DealDamage(Vector3 position, TankManager tankManager){
        var damage = ComputeDamage(position);
        tankManager.ApplyDamage(damage);
    }

    [Command]
    private void Cmd_PlayExplosion(){
        var explosionInstance = Instantiate(explosionParticles, transform.position, transform.rotation);
        explosionInstance.GetComponent<ParticleSystem>().Play();
        Destroy(explosionInstance.gameObject, 1f);
        NetworkServer.Spawn(explosionInstance);
    }

    private float ComputeDamage(Vector3 targetPosition){
        var explosionToTarget = targetPosition - transform.position;
        var explosionDistance = explosionToTarget.magnitude;
        var relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        var damage = Mathf.Max(0f, relativeDistance * maxDamage);

        return damage;
    }
}