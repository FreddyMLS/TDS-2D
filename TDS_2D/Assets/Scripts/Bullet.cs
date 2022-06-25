using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public GameObject effects;

    private GameObject sc_m;
    private GameObject sc;

    [SerializeField] bool enemyBullet;

    void Start()
    {
        Invoke("DestroyBullet", lifetime);
        sc = GameObject.FindGameObjectWithTag("Player");
        sc_m = GameObject.Find("MonstersGunMan");
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<Player>().ChangeHealth(-damage);
            }

            DestroyBullet();
            
        }
        if (!enemyBullet)
        {
            Vector3 Scaler = sc.transform.localScale;
            //чтобы пуля летела правильно
            if (Scaler.x > 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                if (Scaler.x < -1)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }
        }
        else if(sc_m!=null)
        {
            Vector3 Scaler1 = sc_m.transform.localScale;
            //чтобы пуля врага летела правильно
            if (Scaler1.x >= 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                if (Scaler1.x <= -1)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }
        }
    }

    public void DestroyBullet()
    {
        Instantiate(effects, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
