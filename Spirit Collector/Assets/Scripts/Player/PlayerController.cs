using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;

    private Camera mainCam;
    private Rigidbody2D rb;
    private Vector2 targetMousePos;
    private Vector2 movementVec;
    private bool enableShoot = true;

    [Header("Movement Settings")]
    [SerializeField] private float shootInterval = 0.25f;
    [SerializeField] private float rotSpeed = 10.0f;
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("Shoot")]

    void Awake()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetMovement();
        SetMousePosition();
        PlayerShoot();
        PlayerRotation();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    //Shooting
    public void PlayerShoot()
    {
        if (!enableShoot) return;

        if (playerInputManager.IsShootPressed())
        {
            StartCoroutine(EnableShoot(shootInterval));
        }
    }

    IEnumerator EnableShoot(float sec)
    {
        enableShoot = false;

        yield return new WaitForSeconds(sec);

        enableShoot = true;
    }

    //Rotation
    public void PlayerRotation()
    {
        Quaternion currentRotation = transform.rotation;

        Vector2 mouseDirection = targetMousePos - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    //Movement
    public void PlayerMovement()
    {
        float horizontalInput = movementVec.x;
        float verticalInput = movementVec.y;

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f) * moveSpeed;

        rb.AddForce(movement);
    }

    //Set Input Vectors
    public void SetMousePosition()
    {
        targetMousePos = mainCam.ScreenToWorldPoint(playerInputManager.GetMousePosition());
    }

    public void SetMovement()
    {
        movementVec = playerInputManager.GetMovement();
    }
}
