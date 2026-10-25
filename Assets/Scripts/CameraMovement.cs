using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public Transform player;       // Arrastra aquí el objeto del Jugador desde la Jerarquía
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Distancia relativa entre la cámara y el jugador
    public float smoothSpeed = 5f; // Velocidad de suavizado del movimiento

    [Header("Rotación con Flechitas")]
    public float rotationSpeed = 120f; // Velocidad de giro al presionar las flechas
    private float currentAngle = 0f;   // Ángulo actual de rotación horizontal (Yaw)

    [Header("Rotación Vertical (Límites)")]
    public float minVerticalAngle = -10f; // Límite hacia abajo (ángulo mínimo)
    public float maxVerticalAngle = 40f;  // Límite hacia arriba (ángulo máximo)
    private float currentVerticalAngle = 0f; // Ángulo actual de rotación vertical (Pitch)

    void Update()
    {
        // Rotar la cámara a la izquierda o derecha usando las flechitas horizontales
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAngle += rotationSpeed * Time.deltaTime;
        }

        // Rotar la cámara hacia arriba o abajo usando las flechitas verticales
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentVerticalAngle -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentVerticalAngle += rotationSpeed * Time.deltaTime;
        }

        // Aplicar los límites (Clamp) para que la cámara no se pase de largo al subir o bajar
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, minVerticalAngle, maxVerticalAngle);
    }

    // LateUpdate se ejecuta después de que todos los movimientos del jugador hayan terminado
    void LateUpdate()
    {
        if (player == null) return;

        // Crear una rotación basada tanto en el ángulo vertical como en el horizontal
        Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentAngle, 0f);

        // Aplicar la rotación al offset para que la cámara orbite con inclinación
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