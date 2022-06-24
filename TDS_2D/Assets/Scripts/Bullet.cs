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

    void Start()
    {
        
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
            Instantiate(effects, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        var sc = GameObject.FindGameObjectWithTag("Player");
        Vector3 Scaler = sc.transform.localScale;
        //������������� �����
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
}
