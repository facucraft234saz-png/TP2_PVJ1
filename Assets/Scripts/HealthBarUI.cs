
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerHealth playerHealth;
    public Image healthFill;
    public TMP_Text healthText;

    void Update()
    {
        if (playerHealth == null)
            return;

        float current = playerHealth.currentHealth;
        float maximum = playerHealth.maxHealth;

        // Actualizar el relleno de la barra
        if (healthFill != null && playerHealth.maxHealth > 0f)
        {
            healthFill.fillAmount =
                Mathf.Clamp01(
                    playerHealth.currentHealth /
                    playerHealth.maxHealth
                );
        }

        // Actualizar el texto de vida
        if (healthText != null)
        {
            healthText.text =
                Mathf.CeilToInt(current) + "/" +
                Mathf.CeilToInt(maximum) + " ♥";
        }
    }
}
