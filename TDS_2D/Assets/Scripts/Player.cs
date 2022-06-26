using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public bool facingRight = true;
    public float speed;

    [Header("Health")]
    public int health;
    public Text healhDisplay;
    public GameObject Botll_Ef;
    
    [Header("Shield")]
    public GameObject shield;
    public Shield shieldTimer;
    public GameObject Shield_Ef;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); //Получение компонетнов из аниматора
        rb = GetComponent<Rigidbody2D>(); //Получение компонента RB2D
    }

    void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical); // Считывает положение
        moveVelocity = moveInput.normalized * speed; // конечная скорость

        if (moveInput.x == 0) // Если стоит, идет
        {
            anim.SetBool("inRunning", false); //Стандартная анимация
        }
        else
        {
            anim.SetBool("inRunning", true); // Анимация бега
        }

        if(!facingRight && moveInput.x > 0)
        {
            Flip();
        }
        else if(facingRight && moveInput.x < 0)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // движение самого играка
    }

    private void Flip() // Разворот пресонажа
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Botlle"))
        {
            ChangeHealth(5);
            Instantiate(Botll_Ef, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            if (!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                Instantiate(Shield_Ef, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Instantiate(Shield_Ef, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
            
        }
    }

    public void ChangeHealth(int healthValue)
    {
        if (!shield.activeInHierarchy || shield.activeInHierarchy && healthValue > 0)
        {
            health += healthValue;
            healhDisplay.text = "HP: " + health;
        }
        else if(shield.activeInHierarchy && healthValue < 0)
        {
            shieldTimer.ReduceTime(healthValue);
        }

    }
}
