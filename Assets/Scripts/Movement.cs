using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform[] rayStartPoints;
    void Start()
    {
        //rigidbody değerini Player objesinin rigidbody'si olacak şekilde tanımladık
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        TakeInput();
        OnGroundCheck();
    }

    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            //x değerini player'ın x değeri yapmazsak zıpladığımızda dururuz.
            rb.velocity = new Vector3(rb.velocity.x,Mathf.Clamp((jumpPower * 100) * Time.deltaTime,0f,15f), 0f);          
        }

        //GetKey bastığında ve basılı tuttuğunda kodu çalıştırır
        //KeyCode'u unttuysak string olarak tuşu yazabiliriz GetKey(KeyCode."a")
        if (Input.GetKey(KeyCode.A))
        {
            //y değerini player'ın y değeri yapmazsak zıpladığımızda sağa sola gidemeyiz
            rb.velocity = new Vector3(Mathf.Clamp((speed*100) * Time.deltaTime,0f,15f), rb.velocity.y, 0f);
            //turnSpeed Time.deltaTime ile çarpılmazsa anında dönül gösterir Lerp'ün anlmaı olmaz
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -89.99f, 0f), turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //y değerini player'ın y değeri yapmazsak zıpladığımızda sağa sola gidemeyiz
            //hareketimiz eksi yönüne doğru olduğu için Mathf.Clamp'in minimum değeri -15 max değeri 0 olmalıdır.
            rb.velocity = new Vector3(Mathf.Clamp((-speed*100) * Time.deltaTime,-15f,0f), rb.velocity.y, 0f);
            //turnSpeed Time.deltaTime ile çarpılmazsa anında dönül gösterir Lerp'ün anlmaı olmaz
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 89.99f, 0f), turnSpeed * Time.deltaTime);
        }
        //Tuşa basılmadığında karakter dursun
        else
        {   //Havada asılı kalmaması için y değerini o anki y değerine eşitledik.
            rb.velocity= new Vector3(0f,rb.velocity.y,0f);
        }
    }

    private bool OnGroundCheck()
    {
        bool hit = false;

        for (int i = 0; i < rayStartPoints.Length; i++)
        {
            hit = Physics.Raycast(rayStartPoints[i].position, -rayStartPoints[i].transform.up, 0.25f);
            Debug.DrawRay(rayStartPoints[i].position, -rayStartPoints[i].transform.up * 0.25f, Color.red);
        }
        
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}