using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float offset=0;
    public GameObject bullet;
    public Transform shotPoint;
    public Joystick joystick;

    private float timeBtwShots;
    public float staetTimeBtwShots;

    void Start()
    {

    }

    
    void Update()
    {
        var sc = GameObject.FindGameObjectWithTag("Player");
        Vector3 Scaler = sc.transform.localScale;
        Vector3 pos = sc.transform.position;

        //автонаведение пушки
        if (Scaler.x>1)
        {
            offset = 0;
        }
        else
        {
            if (Scaler.x < -1) 
            { 
                offset = 180;  
            }
        }
        var enemyObject = GameObject.FindWithTag("Enemy");
        var dir = enemyObject.transform.position - transform.position;
        float rotz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotz+offset, Vector3.forward);



        //Стрельба
        if (timeBtwShots <= 0)
        {
            if (joystick.Horizontal == 0 && joystick.Vertical == 0)
            {
                Shoot();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;

        }
        
    }
    void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShots = staetTimeBtwShots;
    }
}
