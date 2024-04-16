using UnityEngine;

public class TitleScreenFish : MonoBehaviour
{
    public float speed = 2f; // Speed of fish movement
    public Vector2 swimAreaSize = new Vector2(18f, 10f); // Size of the area where fish can swim
    public float minWaitTime = 0f; // Minimum time between each movement
    public float maxWaitTime = 0f; // Maximum time between each movement
    public float rotationSpeed = 5f; // Speed of rotation

    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float waitTime;
    private bool isMoving = false;

    void Start()
    {
        // Set initial random target position
        SetRandomTargetPosition();
        startPosition = transform.position;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        transform.Rotate(0, 0, 90);
    }

    void Update()
    {
        if (!isMoving)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                SetRandomTargetPosition();
                isMoving = true;
            }
        }
        else
        {
            MoveFish();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-swimAreaSize.x / 2f, swimAreaSize.x / 2f);
        float randomY = Random.Range(-swimAreaSize.y / 2f, swimAreaSize.y / 2f);
        targetPosition = new Vector3(randomX, randomY, 0);
        waitTime = Random.Range(minWaitTime, maxWaitTime);

        // Calculate the direction to the target position accounting for initial rotation
        Quaternion initialRotation = Quaternion.Euler(0, 0, -90);
        Vector3 rotatedDirection = initialRotation * (targetPosition - transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveFish()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotate the fish to face the movement direction
        Quaternion initialRotation = Quaternion.Euler(0, 0, -90);
        Vector3 rotatedDirection = initialRotation * (targetPosition - transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}