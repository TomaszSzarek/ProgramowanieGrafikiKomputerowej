using UnityEngine;
using UnityEngine.UI;

public class BloodScreenEffectUI : MonoBehaviour
{
    public static BloodScreenEffectUI Instance;
    public float displayTime = 1.0f; // Czas wyœwietlania efektu
    private float timer = 0.0f;
    public RawImage rawImage;

    void Start()
    { 
        Instance = this;
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (timer > 0)
        {
            // Zmiana koloru na bardziej czerwony z up³ywem czasu
            float redIntensity = Mathf.Lerp(0.5f, 0.0f, (displayTime - timer) / displayTime);
            rawImage.color = new Color(1.0f, 0.0f, 0.0f, redIntensity);

            // Zanikanie koloru z czasem
            timer -= Time.deltaTime;
        }
        else
        {
            // Resetowanie koloru
            rawImage.color = Color.clear;
        }
    }

    public void TriggerBloodEffect()
    {
        timer = displayTime;
    }
}
