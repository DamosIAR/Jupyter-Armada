using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime = 1f;
    [SerializeField] private ParticleSystem bulletParticle;
    private ParticleSystem bulletParticleInstance;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            summonParticle();
            Destroy(gameObject);
        }
    }

    private void summonParticle()
    {
        bulletParticleInstance = Instantiate(bulletParticle, transform.position, Quaternion.identity);
    }

    IEnumerator destroyParticle(float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        Destroy(bulletParticleInstance.gameObject);
    }
}
