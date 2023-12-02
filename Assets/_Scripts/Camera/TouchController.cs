using Cinemachine;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private TouchField touchField;
    [SerializeField] private float SensivityX;
    [SerializeField] private float SensivityY;

    void Start()
    {
        SensivityX = PlayerPrefs.GetFloat("HorizontalSensivity", 0.2f);
        SensivityY = PlayerPrefs.GetFloat("VerticalSensivity", 0.2f);
    }

    void Update()
    {
        freeLookCamera.m_XAxis.Value += touchField.TouchDist.x * 200 * SensivityX * Time.deltaTime;
        freeLookCamera.m_YAxis.Value += touchField.TouchDist.y * -SensivityY * Time.deltaTime;
    }

    public void SetSensivity(float sensivityX, float sensivityY)
    {
        SensivityX = sensivityX;
        SensivityY = sensivityY;
    }
}