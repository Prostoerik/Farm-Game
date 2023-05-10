using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // —корость движени€ животных
    public float changeDirectionInterval = 2f; // »нтервал времени дл€ смены направлени€ движени€
    private float elapsedTime = 0f; // ѕрошедшее врем€ с последней смены направлени€
    private Vector2 currentDirection; // “екущее направление движени€
    public Animator animator; // јнимаци€ движени€

    private void Start()
    {
        currentDirection = GetRandomDirection(); // Ќачальное случайное направление
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= changeDirectionInterval)
        {
            currentDirection = GetRandomDirection(); // ѕолучаем новое случайное направление
            elapsedTime = 0f; // ќбнул€ем врем€ с последней смены направлени€
        }

        AnimateMovement(currentDirection);

        // ѕеремещаем животное в текущем направлении с учетом скорости
        transform.Translate(currentDirection * movementSpeed * Time.deltaTime);
    }

    private Vector2 GetRandomDirection()
    {
        // ѕолучаем случайное направление
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        return new Vector2(randomX, randomY).normalized; // Ќормализуем вектор дл€ посто€нной скорости
    }

    private void AnimateMovement(Vector2 direction)
    {
        //проверка что контролер дл€ анимации присутствует
        if (animator != null)
        {
            //проверка на движение
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