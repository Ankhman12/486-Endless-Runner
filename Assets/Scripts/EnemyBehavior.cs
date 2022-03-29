using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{ 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) //Player Layer
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.hitSound);
            // Add a random symptom to the player's list of symptoms
            GameManager.Instance.AddRandomSymptom();
            // And subtract a single life
            GameManager.Instance.DamagePlayer();
        }
    }
}
