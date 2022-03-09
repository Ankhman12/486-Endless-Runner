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

    private Volume postProcessorVolume;
    private float postProcessorWeight;
    private Image blackScreenImage;

    void Start()
    {
        postProcessorVolume = postProcessor.GetComponent<Volume>();
        postProcessorWeight = postProcessorVolume.weight;
        blackScreenImage = blackScreen.GetComponent<Image>();
    }

    void fadeToBlack()
    {
        // Increase the weight of the post processor
        postProcessorVolume.weight += 0.001f;
        // Increase the alpha of the black screen
        blackScreenImage.color = new Color(0, 0, 0, 0.001f + blackScreenImage.color.a);
    }
}
