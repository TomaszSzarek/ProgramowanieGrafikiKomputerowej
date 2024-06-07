using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Collections;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    [SerializeField] public float currentStamina;
    [SerializeField] public float currentHealth;
    public Slider healthSlider;//
    public Slider staminaSlider;
    public Button btn;

    void Start()
    {
        btn.gameObject.SetActive(false);
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        UpdateHealth();
        if (currentHealth <= 0)
        {
            Die();
        }
        /*if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetDamage(20f);
        }*/
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Heal(5f);
        }
        if(currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public void UpdateHealth()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth,maxHealth);
    }

    public void TakeStamina(float staminaAmount)
    {
        currentStamina -= staminaAmount;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, maxStamina);
    }

    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, maxHealth);
    }

    public void RegenStamina(float staminaAmount)
    {
        currentStamina += staminaAmount;
        currentStamina = Mathf.Clamp(currentStamina, 0, 100);
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, maxStamina);
    }

    public void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    private void Die()
    {
        btn.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Gracz zgin¹³!");
        SceneManager.LoadScene(1);
    }
}