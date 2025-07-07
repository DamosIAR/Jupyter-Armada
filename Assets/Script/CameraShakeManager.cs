using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField] private float ShakeForce = 0.01f;
    public static CameraShakeManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSource, float shakeForce)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }
}
