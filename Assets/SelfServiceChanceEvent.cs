using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SelfServiceChanceEvent : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public static event Action<float> OnSliderValueChangedEvent;
    //Obejœcie ¿eby element posiada³ wartoœæ jeszcze przed jego zbudowaniem (strasznie nieeleganckie
    public static float ServiceChance = 40;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 40;
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
        ServiceChance = value;
        OnSliderValueChangedEvent?.Invoke(value);
    }
}
