using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private PlayerScriptableObject playerScriptable;

    private Camera mainCam;
    private Rigidbody2D playerRb;
    private bool enableShoot = true;

    private Vector2 targetMousePos;
    private Vector2 movementVec;

    private Vector2 shotPointVec;

    private float rotSpeed;
    private float moveSpeed;

    private float shootingRate;

    private bool isLight = true;

    [SerializeField] private Transform shotPoint;

    void Awake()
    {
        mainCam = Camera.main;
        playerRb = GetComponent<Rigidbody2D>();
        rotSpeed = playerScriptable.rotSpeed;
        moveSpeed = playerScriptable.moveSpeed;
        shootingRate = playerScriptable.shootingRate;
    }

    void Update()
    {
        SetMovement();
        SetMousePosition();
        PlayerShoot();
        PlayerRotation();
        ChangeType();

        shotPointVec = shotPoint.position;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    //Shooting
    private void PlayerShoot()
    {
        if (!enableShoot) return;

        if (playerInputManager.IsShootPressed())
        {
            BasePool.Instance.bulletCount++;
            GameObject bulletClone = BasePool.Instance.playerBulletPool.Get();
            bulletClone.transform.position = shotPointVec;
            bulletClone.transform.rotation = transform.rotation;
            StartCoroutine(EnableShoot(shootingRate));
        }
    }

    IEnumerator EnableShoot(float sec)
    {
        enableShoot = false;

        yield return new WaitForSeconds(sec);

        enableShoot = true;
    }

    //Rotation
    private void PlayerRotation()
    {
        Quaternion currentRotation = transform.rotation;

        Vector2 mouseDirection = targetMousePos - (Vector2)transform.position;
        float targetAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    //Movement
    private void PlayerMovement()
    {
        float horizontalInput = movementVec.x;
        float verticalInput = movementVec.y;

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f) * moveSpeed;

        playerRb.AddForce(movement);
    }

    //Change
    private void ChangeType()
    {
        if (playerInputManager.IsChangePressed())
        {
            isLight = !isLight;

            if (isLight)
            {
                GetComponent<SpriteRenderer>().sprite = playerScriptable.sprites[0];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = playerScriptable.sprites[1];
            }
        }
    }

    //Set Input Vectors
    private void SetMousePosition()
    {
        targetMousePos = mainCam.ScreenToWorldPoint(playerInputManager.GetMousePosition());
    }

    private void SetMovement()
    {
        movementVec = playerInputManager.GetMovement();
    }
}
