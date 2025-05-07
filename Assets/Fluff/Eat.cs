using UnityEngine;

public class Eat : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 3f;
    public float trustGain = 1f;

    private Animator animator;
    private bool isEating = false;

    private FollowPlayerWhenTrusted trustSystem; // Reference to the trust script

    void Start()
    {
        animator = GetComponent<Animator>();
        trustSystem = GetComponent<FollowPlayerWhenTrusted>(); // 👈 Make sure it's on the same GameObject
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (Input.GetKeyDown(KeyCode.F) && !isEating && distance <= maxDistance)
        {
            Debug.Log("F pressed near creature — start eating");

            isEating = true;
            animator.SetBool("IsEating", true);

            // ✅ Increase trust when starting to eat
            if (trustSystem != null)
            {
                trustSystem.IncreaseTrust(trustGain);
            }

            Invoke(nameof(ResetEating), 2f); // Optional: reset after 2 seconds
        }
    }

    void ResetEating()
    {
        isEating = false;
        animator.SetBool("IsEating", false);
        Debug.Log("Finished Eating");
    }
}
