using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject SpecialbulletPrefab;
    public Transform firePoint;
    public float force = 20f;
    public float specialforce = 10f;

    private CinemachineImpulseSource impulseSource;
    //public GameObject UltPrefab;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.right *  force, ForceMode.Impulse);

    }

    public void SpecialSkillFire()
    {
        CameraShakeManager.instance.CameraShake(impulseSource, 2f);
        GameObject crescentBullet = Instantiate(SpecialbulletPrefab, firePoint.position, firePoint.rotation);
        crescentBullet.GetComponent<Rigidbody>().AddForce(firePoint.right * specialforce, ForceMode.Impulse);
    }
}
