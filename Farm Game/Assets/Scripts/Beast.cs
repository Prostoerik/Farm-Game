using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    public float moveSpeed = 3f; 
    public float idleTime = 2f; 
    public Vector2 boundsMin; 
    public Vector2 boundsMax; 

    private bool isMoving = true;
    private Vector3 targetPosition;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetRandomTarget();
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
            animator.SetBool("isMoving", false);
            Invoke(nameof(SetRandomTarget), idleTime);
        }
        else
        {
            UpdateAnimatorParameters();
        }
    }

    private void SetRandomTarget()
    {
        isMoving = true;
        animator.SetBool("isMoving", true);

        float randomX = Random.Range(boundsMin.x, boundsMax.x);
        float randomY = Random.Range(boundsMin.y, boundsMax.y);

        targetPosition = new Vector3(
            Mathf.Clamp(randomX, boundsMin.x, boundsMax.x),
            Mathf.Clamp(randomY, boundsMin.y, boundsMax.y),
            transform.position.z
        );

        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float horizontal = direction.x;
        float vertical = direction.y;

        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
    }
}
