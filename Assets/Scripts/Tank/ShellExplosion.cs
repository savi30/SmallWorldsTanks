using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShellExplosion : NetworkBehaviour  
{
    public string playerTag = "Player";
    public GameObject explosionParticles;

    public float maxDamage = 50f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider[] coliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i].tag == playerTag)
            {
                Rigidbody target = coliders[i].GetComponent<Rigidbody>();
                if (!target)
                    continue;
                
                target.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                
                TankHealth targetHealth = target.GetComponent<TankHealth>();
                if (!targetHealth)
                    continue;

                float damage = ComputeDamage(target.position);
                targetHealth.ApplyDamage(damage);
            }
        }
        Cmd_PlayExplosion();
        Destroy(gameObject);
    }

    [Command]
    private void Cmd_PlayExplosion()
    {
        GameObject explosionInstance = Instantiate(explosionParticles, transform.position, transform.rotation);
        explosionInstance.GetComponent<ParticleSystem>().Play();
        Destroy(explosionInstance.gameObject, 1f);
        NetworkServer.Spawn(explosionInstance);
    }

    private float ComputeDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistange = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistange) / explosionRadius;
        float damage = Mathf.Max(0f, relativeDistance * maxDamage);

        return damage;
    }

}
