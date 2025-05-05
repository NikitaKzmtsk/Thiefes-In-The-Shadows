using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player1; // Ссылка на первого игрока
    public Transform player2; // Ссылка на второго игрока
    public float detectionRadius = 10f; // Радиус обнаружения
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool isPlayer1InSight = false;
        bool isPlayer2InSight = false;

        // Проверка на наличие первого игрока в радиусе обнаружения
        if (Vector3.Distance(transform.position, player1.position) < detectionRadius)
        {
            // Проверка, видит ли первый игрок монстра
            isPlayer1InSight = IsPlayerLookingAtMonster(player1);
        }

        // Проверка на наличие второго игрока в радиусе обнаружения
        if (Vector3.Distance(transform.position, player2.position) < detectionRadius)
        {
            // Проверка, видит ли второй игрок монстра
            isPlayer2InSight = IsPlayerLookingAtMonster(player2);
        }

        // Если оба игрока не смотрят на монстра, ищем ближайшего отвернутого игрока
        if (!isPlayer1InSight && !isPlayer2InSight)
        {
            MoveToNearestPlayer();
            return; // Выход из метода
        }

        // Если хотя бы один игрок смотрит на монстра, останавливаемся
        if (isPlayer1InSight || isPlayer2InSight)
        {
            navMeshAgent.ResetPath();
            return; // Выход из метода, так как монстр должен стоять
        }

        // Если ни один игрок не смотрит на монстра и находится в радиусе обнаружения, движемся к ближайшему игроку
        if (Vector3.Distance(transform.position, player1.position) < detectionRadius)
        {
            navMeshAgent.SetDestination(player1.position);
            return; // Выход из метода
        }

        if (Vector3.Distance(transform.position, player2.position) < detectionRadius)
        {
            navMeshAgent.SetDestination(player2.position);
            return; // Выход из метода
        }

        // Если оба игрока вне радиуса обнаружения, останавливаемся
        navMeshAgent.ResetPath();
    }

    // Метод для перемещения к ближайшему отвернутому игроку
    private void MoveToNearestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        // Определяем ближайшего игрока
        if (distanceToPlayer1 < distanceToPlayer2)
        {
            navMeshAgent.SetDestination(player1.position);
        }
        else
        {
            navMeshAgent.SetDestination(player2.position);
        }
    }

    // Метод для проверки, смотрит ли игрок на монстра
    private bool IsPlayerLookingAtMonster(Transform player)
    {
        Vector3 directionToMonster = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, directionToMonster);

        // Условие угла, при котором игрок "смотрит" на монстра
        return angle < 45f; // Можно настроить под свои нужды
    }
}
