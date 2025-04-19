using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 700f;
    public float collectDuration = 2f; // Toplama süresi
    public float collectDistance = 1.5f; // Topa yakýn olma mesafesi

    private CharacterController characterController;
    private Camera mainCamera;
    private Animator animator;

    private Vector3 moveDirection;
    private bool isCollecting = false;
    private int score = 0; // Puan
    private GameObject nearestBall;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;
        Debug.Log(CheckGroundedRaycast());
        if(!CheckGroundedRaycast())
        {
            characterController.Move(new Vector3(0, -500, 0));
        }

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Hareket
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Dönüþ
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        // Animasyonlar
        if (isCollecting)
        {
            // Sadece toplama animasyonu çalýþsýn
            animator.SetBool("isRunning", false);
            animator.SetBool("isCollecting", true);
        }
        else
        {
            bool isRunning = moveDirection.magnitude > 0.1f;
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isCollecting", false);
        }

        // Toplama baþlat
        if (Input.GetKeyDown(KeyCode.F) && !isCollecting)
        {
            StartCollecting();
        }

        // Topa yaklaþma ve toplama iþlemi
        if (nearestBall != null)
        {
            float distance = Vector3.Distance(transform.position, nearestBall.transform.position);
            if (distance <= collectDistance && !isCollecting)
            {
                StartCollecting();
                Destroy(nearestBall);  // Topu yok et
                score++;               // Skoru artýr
                UpdateScoreUI();       // Skoru UI'da göster
            }
        }

        // Topu bulma iþlemi
        nearestBall = FindNearestBall();
    }

    void StartCollecting()
    {
        isCollecting = true;
        animator.SetBool("isCollecting", true);
        Invoke(nameof(StopCollecting), collectDuration); // Belirli süre sonra durdur
    }

    void StopCollecting()
    {
        isCollecting = false;
        animator.SetBool("isCollecting", false);
    }

    GameObject FindNearestBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject nearest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(transform.position, ball.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearest = ball;
            }
        }

        return nearest;
    }
    public float distance;
    private bool CheckGroundedRaycast()
    {
        return Physics.Raycast(transform.position, Vector3.down, out var hit, distance);
    }


    void UpdateScoreUI()
    {
        // Skor güncellemesi yapýlacak UI burada iþlenecek
        Debug.Log("Score: " + score);
    }
}
