using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Joystick joystick;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Animator anim;
    private bool facingRight = true;

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
}
