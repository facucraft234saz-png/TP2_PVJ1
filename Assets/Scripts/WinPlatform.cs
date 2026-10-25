
using UnityEngine;
using TMPro;

public class WinPlatform : MonoBehaviour
{
    [Header("Referencias")]
    public HackTerminal terminal;
    public Renderer platformRenderer;
    public TMP_Text winText;

    [Header("Colores")]
    public Color offColor = Color.gray;
    public Color onColor = Color.green;

    private bool playerOnPlatform;
    private bool victoryReached;

    void Start()
    {
        if (platformRenderer != null)
            platformRenderer.material.color = offColor;

        if (winText != null)
            winText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckPlayer(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckPlayer(other);
    }

    void OnTriggerExit(Collider other)
    {
        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerOnPlatform = false;

        if (platformRenderer != null)
            platformRenderer.material.color = offColor;

        if (winText != null)
            winText.gameObject.SetActive(false);
    }

    void CheckPlayer(Collider other)
    {
        PlayerHealth playerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerOnPlatform = true;

        if (terminal == null)
        {
            Debug.LogWarning(
                "WinPlatform no tiene una terminal asignada"
            );
            return;
        }

        if (terminal.IsHacked)
        {
            if (platformRenderer != null)
                platformRenderer.material.color = onColor;

            if (winText != null)
            {
                winText.gameObject.SetActive(true);
                winText.text = "Has Ganado";
            }


            if (!victoryReached)
            {
                victoryReached = true;

                Debug.Log("VICTORIA: el jugador completo el objetivo");

                if (GameManager.Instance != null)
                {
                    GameManager.Instance.WinGame();
                }
            }

        }
        else
        {
            if (platformRenderer != null)
                platformRenderer.material.color = offColor;

            if (winText != null)
            {
                winText.gameObject.SetActive(true);
                winText.text =
                    "Debes hackear la terminal para ganar";
            }
        }
    }
}
