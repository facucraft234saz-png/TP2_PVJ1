using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Detección del suelo")]
    public Transform groundPoint;
    public float rayDistance = 0.25f;

    [Header("Capas de Suelo")]
    public LayerMask groundLayer;       // Primera capa (ej. Ground)
    public LayerMask secondGroundLayer; // Segunda capa añadida (ej. Platforms o Metal)

    public bool IsGrounded { get; private set; }

    void Update()
    {
        // Combinamos ambas LayerMasks utilizando el operador | (OR)
        LayerMask combinedLayer = groundLayer | secondGroundLayer;

        IsGrounded = Physics.Raycast(
            groundPoint.position,
            Vector3.down,
            rayDistance,
            combinedLayer
        );
    }

    void OnDrawGizmosSelected()
    {
        if (groundPoint == null) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(
            groundPoint.position,
            groundPoint.position + Vector3.down * rayDistance
        );
    }
}