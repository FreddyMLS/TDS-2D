using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunType gunType;
    public float offset=0;
    public GameObject bullet;
    public Transform shotPoint;
    public Joystick joystick;
    private GameObject sc;
    private GameObject rotate_gun;

    private float timeBtwShots;
    public float staetTimeBtwShots;

    public enum GunType {Default, Enemy}

    void Start()
    {
        sc = GameObject.FindGameObjectWithTag("Player");
        
    }

    
    void Update()
    {
        
        Vector3 Scaler = sc.transform.localScale;

        if (gunType == GunType.Default)
        {

        }
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
        if (gunType == GunType.Default)
        {
            if (enemyObject != null)
            {
                var dir = enemyObject.transform.position - transform.position;
                float rotz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(rotz + offset, Vector3.forward);
            }
        }
        else if(gunType == GunType.Enemy)
        {
            var dir = sc.transform.position - transform.position;
            float rotz = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotz-90, Vector3.forward);
        }
        
        
        
        //Стрельба
        if (timeBtwShots <= 0)
        {
            if (joystick.Horizontal == 0 && joystick.Vertical == 0 && enemyObject!=null)
            {
                Shoot();
            }

            if (gunType == GunType.Enemy)
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
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        timeBtwShots = staetTimeBtwShots;
    }
}
