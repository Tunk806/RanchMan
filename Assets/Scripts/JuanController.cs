using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JuanController : MonoBehaviour
{
    PlayerInputs playInput;
    public float accel = 30f;
    public float turn = 3.5f;

    public InputAction move;


    float accelInput = 0f;
    float turnInput = 0f;
    float rotAngle = 0f;

    Rigidbody2D juanRB;


    Vector2 inputVector;
    private void Awake()
    {
        juanRB = GetComponent<Rigidbody2D>();
        playInput = new PlayerInputs();
    }

    private void OnEnable()
    {
        move = playInput.Player.Move;

        move.Enable();
    }

    void Update()
    {
        inputVector = move.ReadValue<Vector2>();
        accelInput = inputVector.y;
        turnInput = inputVector.x;

    }

    private void FixedUpdate()
    {
        ApplyAccelForce();

        ApplyTurn();
    }

    void ApplyAccelForce()
    {
        Vector2 juanForce = transform.up * accelInput * accel;
        juanRB.AddForce(juanForce, ForceMode2D.Force);
    }
    
    void ApplyTurn()
    {
        rotAngle -= turnInput * turn;
        juanRB.MoveRotation(rotAngle);

    }
}
