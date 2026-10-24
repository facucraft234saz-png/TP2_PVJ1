
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DroneAI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;
    public Transform[] patrolPoints;

    [Header("Configuracion")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float detectionDistance = 10f;
    public float loseDistance = 15f;
    public float waypointDistance = 1f;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool chasing = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent.speed = patrolSpeed;

        if (patrolPoints.Length > 0)
        {
            GoToNextPoint();
        }
    }

    void Update()
    {
        if (player == null || patrolPoints.Length == 0)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // Detectar al jugador
        if (!chasing && distance <= detectionDistance)
        {
            chasing = true;
            agent.speed = chaseSpeed;
            Debug.Log("El dron detecto al jugador");
        }

        // Perseguir al jugador
        if (chasing)
        {
            agent.SetDestination(player.position);

            // Volver a patrullar si se aleja demasiado
            if (distance >= loseDistance)
            {
                chasing = false;
                agent.speed = patrolSpeed;
                GoToNextPoint();

                Debug.Log("El dron volvio a patrullar");
            }
        }
        else
        {
            // Avanzar al siguiente punto de patrulla
            if (!agent.pathPending &&
                agent.remainingDistance <= waypointDistance)
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(
            patrolPoints[currentPoint].position
        );

        currentPoint++;

        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
    }


    void OnDrawGizmosSelected()
    {
        // Dibuja el rango de detección en la ventana Scene
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);

        Gizmos.DrawSphere(
            transform.position,
            detectionDistance
        );

        // Dibuja también el límite para perder al jugador
        Gizmos.color = new Color(1f, 1f, 0f, 0.25f);

        Gizmos.DrawWireSphere(
            transform.position,
            loseDistance
        );
    }


}
