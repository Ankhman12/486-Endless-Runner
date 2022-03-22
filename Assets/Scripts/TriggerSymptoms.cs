using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TriggerSymptoms : MonoBehaviour
{

    [SerializeField] GameManager.Symptom symptomType;

    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AddSymptom(symptomType);
    }
}
