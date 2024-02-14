using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject parent;
    public Transform player;
    public Rigidbody2D badManRB;
    public bool thieved = false;
    public GameObject cowFab;
    public Collider2D trigColl;
    private bool shoot = false;
    private float PlayerDistance = 5;
    public float rotate;
    public float HP = 5;
    private float akchuallyHP;
    public float damage = 1;
    private float chaseTimer = 10;


    private void Awake()
    {
        badManRB = GetComponentInParent<Rigidbody2D>();
        trigColl = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 rot = player.position - transform.position;
        rotate = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg - 90f;
        akchuallyHP = HP;
        if (akchuallyHP == 0 && thieved == false)
        {
            Destroy(parent.gameObject);
            Destroy(this.gameObject);
        }
        if (thieved == true)
        {
            parent.gameObject.tag = "EnemyTake";
            badManRB.velocity = (transform.position - player.transform.position).normalized * 2;
            HP = 1;
        }
        if (thieved == true && akchuallyHP == 0)
        {
            Instantiate<GameObject>(cowFab, parent.transform.position, Quaternion.identity);
            Destroy(parent.gameObject);
            Destroy(this.gameObject);
        }
        if (Vector2.Distance(transform.position, player.position) > PlayerDistance && chaseTimer <= 0)
        {
            chaseTimer = 5;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cow")
        {
            badManRB.velocity = (collision.transform.position - transform.position).normalized * 2.5f;
        }
        else if (Vector2.Distance(transform.position, player.position) < PlayerDistance && chaseTimer >= 0)
        {
            badManRB.velocity = (player.transform.position - transform.position).normalized * 3;
            chaseTimer -= Time.deltaTime;
        }
    }
}