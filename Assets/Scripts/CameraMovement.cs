using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform player;       // Arrastra aquí el objeto del Jugador desde la Jerarquía
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Distancia relativa entre la cámara y el jugador
    public float smoothSpeed = 5f; // Velocidad de suavizado del movimiento

    [Header("Rotación con Flechitas")]
    public float rotationSpeed = 120f; // Velocidad de giro al presionar las flechas
    private float currentAngle = 0f;   // Ángulo actual de rotación orbital

    void Update()
    {
        // Rotar la cámara a la izquierda o derecha usando las flechitas del teclado
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAngle += rotationSpeed * Time.deltaTime;
        }
    }

    // LateUpdate se ejecuta después de que todos los movimientos del jugador hayan terminado
    void LateUpdate()
    {
        if (player == null) return;

        // Crear una rotación basada en el ángulo actual alrededor del eje Y
        Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);

        // Aplicar la rotación al offset para que la cámara orbite alrededor del jugador
        Vector3 rotatedOffset = rotation * offset;

        // Calcula la posición deseada sumando la posición del jugador y el desfase rotado
        Vector3 desiredPosition = player.position + rotatedOffset;

        // Interpola suavemente entre la posición actual y la deseada usando Time.deltaTime
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // Hace que la cámara siempre apunte hacia el jugador
        transform.LookAt(player);
    }
}