using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private TMP_InputField HorizontalSensivityInputField;
    [SerializeField] private TMP_InputField VerticalSensivityInputField;
    [SerializeField] private Slider HorizontalSensivitySlider;
    [SerializeField] private Slider VerticalSensivitySlider;
    [SerializeField] private TouchController touchController;

    void Start()
    {
        inGamePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }


    #region SettingsPanel

    public void DeactiveSettingsPanel()
    {
        settingsPanel.SetActive(false);
        inGamePanel.SetActive(true);
        RemoveEventListeners();
    }

    public void ActiveSettingsPanel()
    {
        settingsPanel.SetActive(true);
        inGamePanel.SetActive(false);
        initSliders();
        AddEventListeners();
    }

    void initSliders()
    {
        HorizontalSensivitySlider.value = PlayerPrefs.GetFloat("HorizontalSensivity", 0.2f);
        VerticalSensivitySlider.value = PlayerPrefs.GetFloat("VerticalSensivity", 0.2f);
        HorizontalSensivityInputField.text = HorizontalSensivitySlider.value.ToString();
        VerticalSensivityInputField.text = VerticalSensivitySlider.value.ToString();
    }

    private void AddEventListeners()
    {
        HorizontalSensivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(HorizontalSensivitySlider, HorizontalSensivityInputField); });
        VerticalSensivitySlider.onValueChanged.AddListener(delegate { OnSliderValueChanged(VerticalSensivitySlider, VerticalSensivityInputField); });

        HorizontalSensivityInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(HorizontalSensivityInputField, HorizontalSensivitySlider); });
        VerticalSensivityInputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(VerticalSensivityInputField, VerticalSensivitySlider); });
    }

    void RemoveEventListeners()
    {
        HorizontalSensivitySlider.onValueChanged.RemoveAllListeners();
        VerticalSensivitySlider.onValueChanged.RemoveAllListeners();
        HorizontalSensivityInputField.onValueChanged.RemoveAllListeners();
        VerticalSensivityInputField.onValueChanged.RemoveAllListeners();
    }

    private void OnInputFieldValueChanged(TMP_InputField inputField, Slider slider)
    {
        float value;

        if (float.TryParse(inputField.text, out value))
        {
            slider.value = value;
        }
    }

    private void OnSliderValueChanged(Slider slider, TMP_InputField inputField)
    {
        inputField.text = slider.value.ToString();
    }

    public void ReturnButton()
    {
        PlayerPrefs.SetFloat("HorizontalSensivity", HorizontalSensivitySlider.value);
        PlayerPrefs.SetFloat("VerticalSensivity", VerticalSensivitySlider.value);
        DeactiveSettingsPanel();
        touchController.SetSensivity(HorizontalSensivitySlider.value, VerticalSensivitySlider.value);
        ActiveInGamePanel();
    }

    #endregion


    #region InGamePanel   
    public void DeactiveInGamePanel()
    {
        inGamePanel.SetActive(false);
    }

    public void ActiveInGamePanel()
    {
        inGamePanel.SetActive(true);
    }
    #endregion

    public void ExitApplication()
    {
        Application.Quit();
    }
}
