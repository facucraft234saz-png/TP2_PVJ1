
using UnityEngine;
using TMPro;

public class HackTerminal : MonoBehaviour
{
    [Header("Configuracion")]
    public float hackDuration = 10f;
    public float detectionRadius = 5f;
    public Color hackedColor = Color.green;

    [Header("Referencias")]
    public Transform player;
    public TMP_Text hackText;
    public Renderer terminalRenderer;

    private bool isNearby;
    private bool isTargeted;
    private bool isHacking;
    private bool isHacked;
    private float hackTimer;

    public bool IsHacked => isHacked;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        if (hackText != null)
            hackText.text = "Presiona E para hackear";

        UpdateTextVisibility();
    }

    void Update()
    {
        // Detectar proximidad sin depender de la camara.
        if (player != null)
        {
            float distance = Vector3.Distance(
                transform.position,
                player.position
            );

            isNearby = distance <= detectionRadius;
        }

        if (isHacking)
        {
            hackTimer += Time.deltaTime;

            float remaining = Mathf.Max(
                0f,
                hackDuration - hackTimer
            );

            if (hackText != null)
            {
                hackText.text = "Hackeando... " +
                    Mathf.CeilToInt(remaining) + "s";
            }

            if (hackTimer >= hackDuration)
                CompleteHack();
        }
        else if (isHacked && hackText != null)
        {
            hackText.text = "Hackeado";
        }
        else if (isNearby && hackText != null)
        {
            hackText.text = "Presiona E para hackear";
        }

        UpdateTextVisibility();
    }

    public void SetTargeted(bool targeted)
    {
        // Se conserva para PlayerInteraction y el Raycast.
        isTargeted = targeted;
        UpdateTextVisibility();
    }

    public void StartHack()
    {
        if (isHacking || isHacked)
            return;

        isHacking = true;
        hackTimer = 0f;
        UpdateTextVisibility();
    }

    void CompleteHack()
    {
        isHacking = false;
        isHacked = true;

        if (terminalRenderer != null)
            terminalRenderer.material.color = hackedColor;

        if (hackText != null)
            hackText.text = "Hackeado";

        UpdateTextVisibility();

        Debug.Log("Terminal hackeada correctamente");
    }

    void UpdateTextVisibility()
    {
        if (hackText == null)
            return;

        hackText.gameObject.SetActive(
            isNearby || isHacking
        );

        if (isHacked && isNearby)
            hackText.text = "Hackeado";
    }
}
