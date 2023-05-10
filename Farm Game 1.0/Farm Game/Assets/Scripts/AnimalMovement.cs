using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // �������� �������� ��������
    public float changeDirectionInterval = 2f; // �������� ������� ��� ����� ����������� ��������
    private float elapsedTime = 0f; // ��������� ����� � ��������� ����� �����������
    private Vector2 currentDirection; // ������� ����������� ��������
    public Animator animator; // �������� ��������

    private void Start()
    {
        currentDirection = GetRandomDirection(); // ��������� ��������� �����������
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= changeDirectionInterval)
        {
            currentDirection = GetRandomDirection(); // �������� ����� ��������� �����������
            elapsedTime = 0f; // �������� ����� � ��������� ����� �����������
        }

        AnimateMovement(currentDirection);

        // ���������� �������� � ������� ����������� � ������ ��������
        transform.Translate(currentDirection * movementSpeed * Time.deltaTime);
    }

    private Vector2 GetRandomDirection()
    {
        // �������� ��������� �����������
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        return new Vector2(randomX, randomY).normalized; // ����������� ������ ��� ���������� ��������
    }

    private void AnimateMovement(Vector2 direction)
    {
        //�������� ��� ��������� ��� �������� ������������
        if (animator != null)
        {
            //�������� �� ��������
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}