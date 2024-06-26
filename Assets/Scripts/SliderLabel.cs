using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderLabel : MonoBehaviour
{
    public Slider slider;
    public Text label;
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { UpdateLabel(); });
        label.text = slider.value.ToString("0.00");
    }

    void UpdateLabel()
    {
        label.text = slider.value.ToString("0.00");
    }
}
