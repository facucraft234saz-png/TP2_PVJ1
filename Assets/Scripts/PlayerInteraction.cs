
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Deteccion por Raycast")]
    public Camera playerCamera;
    public float interactionDistance = 4f;
    public LayerMask interactableLayer;

    [Header("Deteccion por proximidad")]
    public float proximityDistance = 5f;

    private HackTerminal currentTerminal;
    private HackTerminal nearbyTerminal;

    void Update()
    {
        DetectWithRaycast();
        DetectNearbyTerminal();

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Primero se prioriza la terminal que mira el jugador.
            // Si no mira ninguna, usa la terminal cercana.
            HackTerminal terminalToHack =
                currentTerminal != null
                ? currentTerminal
                : nearbyTerminal;

            if (terminalToHack != null)
            {
                terminalToHack.StartHack();
            }
        }
    }

    void DetectWithRaycast()
    {
        currentTerminal = null;

        if (playerCamera == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            interactableLayer))
        {
            currentTerminal =
                hit.collider.GetComponentInParent<HackTerminal>();
        }

        Debug.DrawRay(
            playerCamera.transform.position,
            playerCamera.transform.forward * interactionDistance,
            Color.yellow
        );
    }

    void DetectNearbyTerminal()
    {
        nearbyTerminal = null;

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            proximityDistance,
            interactableLayer
        );

        float closestDistance = float.MaxValue;

        foreach (Collider hit in hits)
        {
            HackTerminal terminal =
                hit.GetComponentInParent<HackTerminal>();

            if (terminal == null)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                terminal.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearbyTerminal = terminal;
            }
        }
    }
}
