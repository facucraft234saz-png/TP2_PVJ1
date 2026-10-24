using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Referencias")]
    public GroundCheck groundCheck;
    public Transform cameraTransform; // Arrastra aquí tu Main Camera

    private Rigidbody rb;
    private float horizontal;
    private float vertical;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Si no asignas la cámara en el inspector, la busca automáticamente
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) &&
            groundCheck != null &&
            groundCheck.IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (cameraTransform != null)
        {
            // Obtener los ejes de la cámara ignorando la altura (Y) para movernos en plano horizontal
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Sincronizar el movimiento del jugador con la dirección de la cámara
            movement = (camForward * vertical + camRight * horizontal).normalized;
        }
        else
        {
            // Movimiento predeterminado por si no encuentra la cámara
            movement = new Vector3(horizontal, 0f, vertical).normalized;
        }

        Vector3 newPosition =
            rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPosition);

        // Opcional: Hace que el jugador gire suavemente hacia la dirección en la que se está moviendo
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 15f * Time.fixedDeltaTime));
        }
    }
}