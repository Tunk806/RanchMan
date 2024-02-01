using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D badManRB;
    private bool shoot = false;
    private float speed = 1f;
    private float pSpeed = 3;
    public float PlayerDistance = 5;
    public float rotate;
    public GameObject parent;

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
        if (collision.tag == "Cow")
        {
            parent.transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < PlayerDistance)
            {
                parent.transform.position = Vector2.MoveTowards(parent.transform.position, player.position, pSpeed * Time.deltaTime);
                badManRB.rotation = rotate;
            }
    }
}
