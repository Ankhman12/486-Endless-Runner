using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: https://www.youtube.com/watch?v=BQGTdRhGmE4
public class CameraShake : MonoBehaviour
{

    [SerializeField] float shakeDuration = 2f;
    [SerializeField] AnimationCurve intensityCurve;

    private bool shaking;

    public void StartShaking()
    {
        shaking = true;
        StartCoroutine("Shaking");
    }

    public void StopShaking()
    {
        shaking = false;
    }

    IEnumerator Shaking()
    {
        while (shaking){
            Vector3 StartPosition = transform.localPosition;
            float elapsedTime = 0f;

            while(elapsedTime < shakeDuration)
            {
                elapsedTime += Time.deltaTime;
                float strength = intensityCurve.Evaluate(elapsedTime / shakeDuration);
                transform.localPosition = StartPosition + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.localPosition = StartPosition;
        }
    }
}
