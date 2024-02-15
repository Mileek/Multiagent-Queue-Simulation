using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonSpeedEvent : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public static event Action<float> OnSliderValueChangedEvent;
    //Obejœcie ¿eby element posiada³ wartoœæ jeszcze przed jego zbudowaniem (strasznie nieeleganckie
    public static float PersonSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0.05f;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(slider.value);//Pierwsze wywo³anie, w razie wypadku
    }

    // Update is called once per frame
    void Update()
    {
        sliderText.text = slider.value.ToString();
    }

    void OnSliderValueChanged(float value)
    {
        PersonSpeed = value;
        OnSliderValueChangedEvent?.Invoke(value);
    }
}
