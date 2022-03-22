using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SymptomNumbness : MonoBehaviour
{
    [SerializeField] GameObject postProcessor;
    [SerializeField] GameObject blackScreen;
    [Range(0f, 1f), SerializeField] float initialVolumeWeight;
    [Range(0f, 1f), SerializeField] float finalVolumeWeight;
    [Range(0f, 1f), SerializeField] float initialOpacity;

    private Volume postProcessorVolume;
    private Image blackScreenImage;
    private bool fading;
    private Coroutine fadeCoroutine;

    void Start()
    {
        postProcessorVolume = postProcessor.GetComponent<Volume>();
        blackScreenImage = blackScreen.GetComponent<Image>();
        resetShading();
    }

    void fadeToBlack()
    {
        if (postProcessorVolume.weight < finalVolumeWeight) {
            // Increase the weight of the post processor
            postProcessorVolume.weight += 0.00025f;
            // Increase the alpha of the black screen
            blackScreenImage.color = new Color(0, 0, 0, 0.00025f + blackScreenImage.color.a);
        }
        else {
            fading = false;
        }
    }

    void resetShading() 
    {
        fading = false;
        postProcessorVolume.weight = initialVolumeWeight;
        blackScreenImage.color = new Color(0, 0, 0, initialOpacity);
    }

    void Update() 
    {   
        // Check to see if the player is on a damaged tag
        if (gameObject.CompareTag("Damaged")) {
            // If the player is on a damaged tag and the post processor is not already fading, fade to black
            if (!fading) {
                fading = true;
                fadeCoroutine = StartCoroutine("fadeToBlack");
            }
        }
    }
}
