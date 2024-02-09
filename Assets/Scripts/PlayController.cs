using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayController : MonoBehaviour
{
    PlayerInputs playInput;

    Rigidbody2D myRB;

    public float turn = 3.5f;

    public InputAction look;
    public InputAction fire;
    public InputAction reload;

    float lookInput;
    float rotAngle;
    float cylAmmo = 6;
    float ammo = 36;

    Vector2 inputVector;

    public GameObject juan;
    public GameObject raycastObject;
    public EnemyController tempEC;
    private Vector3 xy;
    private Vector2 xyTarget;
    Vector3 fwd;
    public TextMeshProUGUI AMMO;
    


    private void Awake()
    {
        playInput = new PlayerInputs();
        myRB = GetComponent<Rigidbody2D>();
        xy.x = transform.position.x;
        xy.y = transform.position.y;
        xy.z = transform.position.z;
    }
    private void OnEnable()
    {
        look = playInput.Player.Look;
        look.Enable();
        fire = playInput.Player.Fire;
        fire.Enable();
        reload = playInput.Player.Reload;
        reload.Enable();
    }
    void Update()
    {
        fwd = raycastObject.transform.TransformDirection(Vector3.up);
        inputVector = look.ReadValue<Vector2>();
        lookInput = inputVector.x;
        AMMO.SetText(cylAmmo + "/" + ammo);
        if (fire.triggered && cylAmmo > 0)
        {
            Debug.DrawRay(raycastObject.transform.position, fwd * 50, Color.red, 1);
            Shoot();
        }
        if (reload.triggered)
        {
            Reload();
        }
        
    }
    private void FixedUpdate()
    {
        ApplyLook();
    }
    void LateUpdate()
    {
        xyTarget.x = juan.transform.position.x;
        xyTarget.y = juan.transform.position.y;
        xy.x = xyTarget.x;
        xy.y = xyTarget.y;

        transform.position = xy;
    }
    void ApplyLook()
    {
        rotAngle -= lookInput * turn;
        myRB.MoveRotation(rotAngle);
    }
    void Shoot()
    {
        int layerMask1 = 1 << 8;
        int layerMask2 = 1 << 2;
        int finalMask = layerMask1 | layerMask2;
        finalMask = ~finalMask;
        cylAmmo--;
            RaycastHit2D hit = Physics2D.Raycast(raycastObject.transform.position, fwd, 50, finalMask);
            if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
            {
                tempEC = hit.collider.gameObject.GetComponentInChildren<EnemyController>();
                tempEC.HP--;
            }
            else
                Debug.Log("Miss");
    }
    void Reload()
    {
        cylAmmo = 6;
        ammo -= 6;
    }
}
    
