using UnityEngine;
using UnityEngine.AI;
using TMPro; // ✅ For TextMeshProUGUI

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class FollowPlayerWhenTrusted : MonoBehaviour
{
    public Transform player;
    public float trust = 0f;
    public float followDistance = 2f;

    private NavMeshAgent agent;
    private Animator animator;

    // ✅ Reference to the UI Text (assign this in the Inspector)
    public TextMeshProUGUI trustText;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Optional: Allow Unity to rotate Fluff based on movement direction
        agent.updateRotation = true;
    }

    void Update()
    {
        // Follow player if trusted enough
        if (trust > 10f && player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }

        // Update walking animation
        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.1f);

        // ✅ Update trust level text on screen
        if (trustText != null)
        {
            trustText.text = $"Trust Level: {trust:F1}";
        }

        // Debug: Draw forward direction
        Debug.DrawRay(transform.position, transform.forward * 2f, Color.green);
    }

    // ✅ External call to increase trust (e.g., from feeding/petting)
    public void IncreaseTrust(float amount)
    {
        trust += amount;
        Debug.Log($"[Fluff] Trust increased by {amount} → Current Trust: {trust}");
    }
}
