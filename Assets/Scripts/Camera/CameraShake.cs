using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    public static CameraShake instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera( float intensity, float shakeTime)
    {
        noise.m_AmplitudeGain = intensity;

        StartCoroutine( StopShaking( shakeTime ) );
    }

    IEnumerator StopShaking( float waitTime )
    {
        yield return new WaitForSeconds( waitTime );

        noise.m_AmplitudeGain = 0;
    }

}
