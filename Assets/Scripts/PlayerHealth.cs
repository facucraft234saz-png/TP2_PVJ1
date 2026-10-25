
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Regeneracion")]
    public float regenerationPerSecond = 1f;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth > 0f && currentHealth < maxHealth)
        {
            currentHealth += regenerationPerSecond * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0f || currentHealth <= 0f)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0f);

        Debug.Log(
            "Vida del jugador: " +
            Mathf.CeilToInt(currentHealth) + "/" +
            Mathf.CeilToInt(maxHealth)
        );


        if (currentHealth <= 0f)
        {
            currentHealth = 0f;

            Debug.Log("El jugador se quedo sin vida");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseGame();
            }
        }
    }


    public void Heal(float amount)
    {
        if (amount <= 0f || currentHealth <= 0f)
            return;

        currentHealth = Mathf.Min(
            currentHealth + amount,
            maxHealth
        );
    }
}
