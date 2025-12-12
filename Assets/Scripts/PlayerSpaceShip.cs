using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpaceShip : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float acceleration = 300f;
    [SerializeField] float friction = 0.5f;

    [Header("Shooting")]
    [SerializeField] GameObject projectilePrefab;
    private AudioSource audioSource;
    public AudioClip fireSound;

    [Header("Controls")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;

    [Header("Control Player Boundaries")]
    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShoot;
    }

    private GameManager gameManager;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        cam = Camera.main;

        // We get the players size to not cut the player in half when colliding with the border of the camera
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        halfWidth = sr.bounds.size.x / 2f;
        halfHeight = sr.bounds.size.y / 2f;
    }

    
    void Update()
    {
        if (gameManager.isGameActive)
        {
            Movement();
            PreventLeavingScreen();    
        }
    }

    void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();
        
        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        shoot.action.started -= OnShoot;
    }

    Vector2 currentVelocity = Vector2.zero;
    const float rawMoveThresholdForBraking = 0.1f;
    void Movement()
    {
        if (rawMove.magnitude < rawMoveThresholdForBraking)
            currentVelocity *= friction * Time.deltaTime;
        
        currentVelocity += rawMove * acceleration * Time.deltaTime;

        float linearVelocity = currentVelocity.magnitude;
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);
    }

    void PreventLeavingScreen()
    {
        Vector3 pos = transform.position;

        // Get the limits of the camera
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, pos.z));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, pos.z));

        // Block leaving the screen
        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + halfHeight, max.y - halfHeight);

        transform.position = pos;
    }

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        audioSource.PlayOneShot(fireSound, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Border"))
    {

    }
}
}
