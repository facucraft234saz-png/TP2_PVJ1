
using UnityEngine;

public class DataCoin : MonoBehaviour
{
    [Header("Configuracion")]
    public int points = 1;
    public float rotationSpeed = 90f;

    void Update()
    {
        // Girar la moneda usando deltaTime
        transform.Rotate(
            Vector3.up * rotationSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player == null)
            return;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(points);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No se encontro ScoreManager en la escena");
        }
    }
}
