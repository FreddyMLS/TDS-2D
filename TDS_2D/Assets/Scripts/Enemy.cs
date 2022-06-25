using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public int damage;
    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    public bool facingRight = true;

    private Player player;
    private Animator anim;
    private Gun point;
    private GameObject points;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if(player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }


        


        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        anim.SetBool("InRunning(MS)", true);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }



    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                OnEnemyAttack();
                anim.SetTrigger("attack_sword");
                anim.SetBool("InRunning(MS)", true);
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    

    public void OnEnemyAttack()
    {
        player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;
    }
}
