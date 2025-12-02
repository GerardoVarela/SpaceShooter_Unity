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

    [Header("Controls")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference shoot;


    void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        // El callback que ocurrira sobre varios metodos que se hayan añadido
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShoot;
    }

    Vector2 currentVelocity = Vector2.zero;
    const float rawMoveThresholdForBraking = 0.1f;
    void Update()
    {
        if (rawMove.magnitude < rawMoveThresholdForBraking)
            currentVelocity *= friction * Time.deltaTime;
        
        currentVelocity += rawMove * acceleration * Time.deltaTime;

        float linearVelocity = currentVelocity.magnitude;
        // Clamp recorta por los lados, ni mas pequenio de 0 ni mas grande de maxSpeed
        linearVelocity = Mathf.Clamp(linearVelocity, 0, maxSpeed);
        currentVelocity = currentVelocity.normalized * linearVelocity;

        transform.Translate(currentVelocity * Time.deltaTime);
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

    Vector2 rawMove;
    private void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.ReadValue<Vector2>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
