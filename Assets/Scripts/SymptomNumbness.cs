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
    private Coroutine fadeCoroutine;

    void Start()
    {
        postProcessorVolume = postProcessor.GetComponent<Volume>();
        blackScreenImage = blackScreen.GetComponent<Image>();
        postProcessorVolume.weight = initialVolumeWeight;
        blackScreenImage.color = new Color(0, 0, 0, initialOpacity);
        resetShading();
        fadeCoroutine = null;
    }

    IEnumerator fadeToBlack()
    {
        float alpha = 0.00025f;
        while (postProcessorVolume.weight < finalVolumeWeight) 
        {
            // Increase the weight of the post processor volume
            postProcessorVolume.weight += alpha;
            // Increase the alpha of the black screen
            blackScreenImage.color = new Color(0, 0, 0, blackScreenImage.color.a + alpha);
            // Wait a frame
            yield return null;
        }
    }

    void resetShading() 
    {
        postProcessorVolume.weight = initialVolumeWeight;
        blackScreenImage.color = new Color(0, 0, 0, initialOpacity);
    }

    public void StartNumbness() {
        if (fadeCoroutine == null)
        {
            fadeCoroutine = StartCoroutine(fadeToBlack());
        }
    }

    public void StopNumbness() {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
            resetShading();
        }
    }
}
