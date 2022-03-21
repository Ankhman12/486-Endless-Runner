using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerSymptoms : MonoBehaviour
{
    public enum Symptom {Tremors, Fatigue, Vision};

    [SerializeField] Symptom symptomType;

    [SerializeField] CameraShake cameraShake;
    
    void OnTriggerEnter(Collider other)
    {
        switch(symptomType)
        {
            case Symptom.Tremors:
                cameraShake.StartShaking();
                break;
            default:
                break;
        }
    }
}
