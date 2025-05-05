using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player1; // ������ �� ������� ������
    public Transform player2; // ������ �� ������� ������
    public float detectionRadius = 10f; // ������ �����������
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool isPlayer1InSight = false;
        bool isPlayer2InSight = false;

        // �������� �� ������� ������� ������ � ������� �����������
        if (Vector3.Distance(transform.position, player1.position) < detectionRadius)
        {
            // ��������, ����� �� ������ ����� �������
            isPlayer1InSight = IsPlayerLookingAtMonster(player1);
        }

        // �������� �� ������� ������� ������ � ������� �����������
        if (Vector3.Distance(transform.position, player2.position) < detectionRadius)
        {
            // ��������, ����� �� ������ ����� �������
            isPlayer2InSight = IsPlayerLookingAtMonster(player2);
        }

        // ���� ��� ������ �� ������� �� �������, ���� ���������� ����������� ������
        if (!isPlayer1InSight && !isPlayer2InSight)
        {
            MoveToNearestPlayer();
            return; // ����� �� ������
        }

        // ���� ���� �� ���� ����� ������� �� �������, ���������������
        if (isPlayer1InSight || isPlayer2InSight)
        {
            navMeshAgent.ResetPath();
            return; // ����� �� ������, ��� ��� ������ ������ ������
        }

        // ���� �� ���� ����� �� ������� �� ������� � ��������� � ������� �����������, �������� � ���������� ������
        if (Vector3.Distance(transform.position, player1.position) < detectionRadius)
        {
            navMeshAgent.SetDestination(player1.position);
            return; // ����� �� ������
        }

        if (Vector3.Distance(transform.position, player2.position) < detectionRadius)
        {
            navMeshAgent.SetDestination(player2.position);
            return; // ����� �� ������
        }

        // ���� ��� ������ ��� ������� �����������, ���������������
        navMeshAgent.ResetPath();
    }

    // ����� ��� ����������� � ���������� ����������� ������
    private void MoveToNearestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        // ���������� ���������� ������
        if (distanceToPlayer1 < distanceToPlayer2)
        {
            navMeshAgent.SetDestination(player1.position);
        }
        else
        {
            navMeshAgent.SetDestination(player2.position);
        }
    }

    // ����� ��� ��������, ������� �� ����� �� �������
    private bool IsPlayerLookingAtMonster(Transform player)
    {
        Vector3 directionToMonster = transform.position - player.position;
        float angle = Vector3.Angle(player.forward, directionToMonster);

        // ������� ����, ��� ������� ����� "�������" �� �������
        return angle < 45f; // ����� ��������� ��� ���� �����
    }
}
