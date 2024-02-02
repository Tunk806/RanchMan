using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowCont : MonoBehaviour
{
    Rigidbody2D cowBody;
    public Transform player;
    public Transform[] enemies;
    public float speed = 5;
    public float startleSpeed = 10;
    public float gravSpeed = 1;
    public float retreatDistance;

    private void Awake()
    {
        cowBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector2 rot = player.position - transform.position;
        float rotate = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg + 90f;
        if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            cowBody.rotation = rotate;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cow")
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, gravSpeed * Time.deltaTime);
        }

    }


}

