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
        anim = GetComponent<Animator>(); //��������� ����������� �� ���������
        rb = GetComponent<Rigidbody2D>(); //��������� ���������� RB2D
    }

    void Update()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical); // ��������� ���������
        moveVelocity = moveInput.normalized * speed; // �������� ��������

        if (moveInput.x == 0) // ���� �����, ����
        {
            anim.SetBool("inRunning", false); //����������� ��������
        }
        else
        {
            anim.SetBool("inRunning", true); // �������� ����
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
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime); // �������� ������ ������
    }

    private void Flip() // �������� ���������
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
