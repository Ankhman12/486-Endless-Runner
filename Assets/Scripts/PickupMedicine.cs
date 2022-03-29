using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMedicine : MonoBehaviour
{
    [SerializeField] GameManager.Symptom symptomType;
    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        GameManager.Instance.RemoveSymptom(symptomType);
        GameManager.Instance.HealPlayer();
    }
}
