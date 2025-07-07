using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBullet : MonoBehaviour
{
    [SerializeField] private float bulletLifetime;

    private CinemachineImpulseSource impulseSource;
    private float CurrentLifetime;

    private void Update()
    {
        CurrentLifetime += Time.deltaTime;
        if(CurrentLifetime >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Wall"))
        {
            Debug.Log("SpecialBulletDestroyed");
            Destroy(gameObject);
        }*/
    }
}
