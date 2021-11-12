using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolSlider : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();
        slider.value = AudioListener.volume;
    }

    private void OnEnable()
    {
        slider.value = AudioListener.volume;
        slider.onValueChanged.AddListener((SliderValue) => AudioListener.volume = SliderValue);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveAllListeners();
    }
}
