using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CowCont : MonoBehaviour
{
    Rigidbody2D cowBody;
    public Transform player;
    public Transform[] enemies;
    public float speed = 5;
    public float startleSpeed = 10;
    public float gravSpeed = 1;
    public float retreatDistance;
    public float HP = 2;
    bool Stole = false;
    public EnemyController tempEC;
    public GameManager gameManager;
    public GameObject gMan;
    private void Awake()
    {
        cowBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gMan = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gMan.GetComponent<GameManager>();
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
        if (HP <= 0 || Stole == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cow")
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, gravSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            tempEC = collision.gameObject.GetComponentInChildren<EnemyController>();
            if (tempEC.Chase == false)
            {
                tempEC.thieved = true;
                tempEC.trigColl.enabled = false;
                Stole = true;
            }
        }
        if (collision.gameObject.tag == "Win")
        {
            gameManager.WIN();
        }
        if (collision.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
        }
    }


}

