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
    public float PlayerDistance = 5;
    public float rotate;
    public float HP = 5;
    public float damage = 1;


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
        if (HP == 0 && thieved == false)
        {
            Destroy(parent.gameObject);
            Destroy(this.gameObject);
        }
        if (thieved == true)
        {
            parent.gameObject.tag = "EnemyTake";
            badManRB.velocity = (transform.position - player.transform.position).normalized * 5;
            HP = 1;
        }
        if (thieved == true && HP == 0)
        {
            Instantiate<GameObject>(cowFab, parent.transform.position, Quaternion.identity);
            Destroy(parent.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cow" && Vector2.Distance(transform.position, player.position) > PlayerDistance)
        {
            badManRB.velocity = (collision.transform.position - transform.position);
        }
        else if (Vector2.Distance(transform.position, player.position) < PlayerDistance)
        {

            badManRB.rotation = rotate;
            badManRB.velocity = (player.transform.position - transform.position);
        }
    }
}