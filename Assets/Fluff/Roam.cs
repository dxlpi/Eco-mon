using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Roam : MonoBehaviour
{
    [Header("Roam Settings")]
    public float roamRadius = 10f;
    public float waitTime = 3f;
    public float arriveThreshold = 0.5f;

    private NavMeshAgent agent;
    private Vector3 origin;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
        animator = GetComponent<Animator>();
        GoToNewDestination();
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0f;
                isWaiting = false;
                GoToNewDestination();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance <= arriveThreshold)
        {
            isWaiting = true;
        }

        float speed = agent.velocity.magnitude;
        animator.SetBool("IsWalking", speed > 0.1f);

        bool isIdle = !animator.GetBool("IsWalking") && !animator.GetBool("IsEating");
        animator.SetBool("IsIdle", isIdle);
    }

    void GoToNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += origin;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
