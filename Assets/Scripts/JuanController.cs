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
    public EnemyController lastE;
    public  float health = 5;
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
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            lastE = collision.gameObject.GetComponentInChildren<EnemyController>();
            health -= lastE.damage;
            juanRB.AddForce((transform.position - collision.transform.position).normalized * 20,ForceMode2D.Impulse);
        }
    }
}
