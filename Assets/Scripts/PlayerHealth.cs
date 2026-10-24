
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del jugador")]
    public int maxHealth = 100;

    public int CurrentHealth { get; private set; }

    void Awake()
    {
        CurrentHealth = maxHealth;
        Debug.Log("Vida del jugador: " + CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || CurrentHealth <= 0)
            return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        Debug.Log("El jugador recibio daño. Vida: " +
                  CurrentHealth + "/" + maxHealth);

        if (CurrentHealth == 0)
        {
            Debug.Log("El jugador se quedo sin vida");
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Min(
            CurrentHealth + amount,
            maxHealth
        );

        Debug.Log("Vida recuperada: " + CurrentHealth);
    }
}
