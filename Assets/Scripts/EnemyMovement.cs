using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints; // Punkty patrolowe
    public float patrolSpeed = 2f; // Prędkość patrolowania
    public float chaseSpeed = 4f; // Prędkość pościgu
    public float detectionRange = 10f; // Zasięg wykrywania gracza
    public Transform player; // Odnośnik do gracza

    private int currentPatrolIndex = 0; // Obecny punkt patrolowy
    private bool isChasing = false; // Czy przeciwnik ściga gracza?

    private void Update()
    {
        if (PlayerInRange())
        {
            // Podążaj za graczem
            ChasePlayer();
        }
        else
        {
            // Patroluj między punktami
            Patrol();
        }
    }

    private bool PlayerInRange()
    {
        // Sprawdź, czy gracz jest w zasięgu wykrywania
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return; // Jeśli nie ma punktów patrolowych, zakończ

        isChasing = false;

        // Pobierz bieżący punkt patrolowy
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        MoveTowards(targetPoint.position, patrolSpeed);

        // Sprawdź, czy przeciwnik dotarł do celu
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            // Przejdź do następnego punktu patrolowego
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        isChasing = true;
        MoveTowards(player.position, chaseSpeed);
    }

    private void MoveTowards(Vector3 target, float speed)
    {
        // Oblicz kierunek i przesuń przeciwnika w stronę celu
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Obróć przeciwnika w stronę celu
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    private void OnDrawGizmosSelected()
    {
        // Wizualizacja zasięgu wykrywania gracza w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
