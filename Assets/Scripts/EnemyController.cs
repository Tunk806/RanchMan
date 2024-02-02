using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject parent;
    public Transform player;
    public Rigidbody2D badManRB;
    private bool shoot = false;
    private float speed = .2f;
    private float pSpeed = .5f;
    public float PlayerDistance = 5;
    public float rotate;
    public float HP = 5;


    private void Awake()
    {
        badManRB = GetComponentInParent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 rot = player.position - transform.position;
        rotate = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg - 90f;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cow" && Vector2.Distance(transform.position, player.position) > PlayerDistance)
        {
            badManRB.AddForce((collision.transform.position - transform.position) * speed);
        }
        else if (Vector2.Distance(transform.position, player.position) < PlayerDistance)
        {
            badManRB.AddForce((player.transform.position - transform.position) * pSpeed);
            badManRB.rotation = rotate;
        }
    }
}