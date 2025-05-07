using UnityEngine;

public class Pet : MonoBehaviour
{
    public string animationTrigger = "Pet";
    public float maxDistance = 3f;

    private Animator animator;
    private Camera playerCamera;
    private FollowPlayerWhenTrusted trustSystem;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        trustSystem = GetComponent<FollowPlayerWhenTrusted>(); // Get trust script
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform == transform)
                {
                    animator.SetTrigger(animationTrigger);
                    if (trustSystem != null)
                        trustSystem.IncreaseTrust(1f);
                }
            }
        }
    }
}
