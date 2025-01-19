using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f; // Prędkość chodzenia
    public float sprintSpeed = 10f; // Prędkość sprintu
    public float crouchSpeed = 2.5f; // Prędkość kucania
    public float jumpForce = 5f; // Siła skoku
    public float gravity = -9.81f;

    [Header("Crouching")]
    public float crouchHeight = 1f; // Wysokość postaci podczas kucania
    private float originalHeight; // Oryginalna wysokość postaci
    private bool isCrouching = false;

    [Header("Wallrun")]
    public float wallRunDuration = 2f; // Czas biegania po ścianie
    public float wallRunSlowdownRate = 0.5f; // Szybkość spowalniania biegu na ścianie
    public float wallRunSpeedMultiplier = 1.5f; // Mnożnik prędkości podczas wallruna
    public LayerMask wallLayer; // Warstwa ścian
    public float wallDetectionDistance = 1f; // Zasięg wykrywania ściany

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isWallRunning = false;
    private float currentWallRunTime = 0f;
    private float moveSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalHeight = characterController.height; // Zachowaj oryginalną wysokość
        moveSpeed = walkSpeed; // Domyślna prędkość to prędkość chodzenia
    }

    void Update()
    {
        Move();

        if (isWallRunning)
        {
            WallRunUpdate();
        }
    }

    void Move()
    {
        // Pobranie wejścia od gracza
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Sprawdzenie sprintu
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            moveSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        // Poruszanie się
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Grawitacja
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Minimalna wartość grawitacji na ziemi
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Skok
        if (Input.GetButtonDown("Jump") && characterController.isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Zastosowanie grawitacji
        characterController.Move(velocity * Time.deltaTime);

        // Wallrun
        if (!characterController.isGrounded && IsNearWall() && Input.GetButton("Jump"))
        {
            StartWallRun();
        }

        // Obsługa kucania
        HandleCrouch();
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            characterController.height = crouchHeight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            characterController.height = originalHeight;
        }
    }

    void StartWallRun()
    {
        if (isWallRunning) return;

        isWallRunning = true;
        currentWallRunTime = wallRunDuration;
        moveSpeed = walkSpeed * wallRunSpeedMultiplier; // Zwiększ prędkość
    }

    void WallRunUpdate()
    {
        // Powolne zmniejszanie prędkości
        currentWallRunTime -= Time.deltaTime;
        moveSpeed -= wallRunSlowdownRate * Time.deltaTime;

        if (currentWallRunTime <= 0 || !IsNearWall())
        {
            StopWallRun();
        }
    }

    void StopWallRun()
    {
        isWallRunning = false;
        moveSpeed = walkSpeed; // Przywrócenie prędkości chodzenia
    }

    bool IsNearWall()
    {
        // Sprawdzenie, czy obok jest ściana
        return Physics.Raycast(transform.position, transform.right, wallDetectionDistance, wallLayer) ||
               Physics.Raycast(transform.position, -transform.right, wallDetectionDistance, wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        // Wizualizacja wykrywania ścian w edytorze
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * wallDetectionDistance);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * wallDetectionDistance);
    }
}
