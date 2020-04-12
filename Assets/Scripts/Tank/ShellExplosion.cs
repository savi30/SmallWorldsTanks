using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public string playerTag = "Player";
    public ParticleSystem explosionParticles;

    public float maxDamage = 50f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider[] coliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < coliders.Length; i++)
        {
            if (coliders[i].tag.Equals(playerTag))
            {
                Rigidbody target = coliders[i].GetComponent<Rigidbody>();
                if (!target)
                    continue;
                target.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            }
        }
        ParticleSystem explosionInstance = Instantiate(explosionParticles, transform.position, transform.rotation);
        explosionInstance.Play();
        Destroy(explosionInstance.gameObject, explosionParticles.main.duration);
        Destroy(gameObject);
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
