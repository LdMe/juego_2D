using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;//rigid body para añadir fisicas
    Animator anim;//controlador de animaciones
    public int speed;//velocidad maxima
    public GameObject shotPrefab;//prefab de proyectil
    public bool jumping;// esta saltando?
    public int jumpForce = 300;
    // Start is called before the first frame update
    void Start()
    {
        jumping = true;
        //buscamos los objetos dentro del personaje
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Shoot()
    {
        GameObject shot = Instantiate(shotPrefab);// crear objeto de tipo shotPrefab
        shot.transform.position = transform.position;//igualar posicion objeto a jugador
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x,0)*20;// damos velocidad al shot
        Destroy(shot, 5);// destruimos el objeto en 5 segundos
    }
    void Jump()
    {
        //
        
        rb.AddForce(new Vector2(0, 1) * jumpForce);//añadimos fuerza vertical al rigid body

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {

            if (!(jumping))
            {
                if (anim.GetCurrentAnimatorClipInfo(0).ToString() == "idle")
                {
                    anim.Play("run");
                }
                anim.SetBool("running", true);
                if (rb.velocity.x > -speed)
                {
                    Vector2 temp = transform.localScale;
                    temp.x = -Mathf.Abs(temp.x);
                    transform.localScale = temp;
                    rb.AddForce(new Vector2(-1, 0) * 1500 * Time.deltaTime);
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) < 2)
                {
                    Vector2 temp = transform.localScale;
                    temp.x = -Mathf.Abs(temp.x);
                    transform.localScale = temp;
                }
            }
        }
        else if (Input.GetKey("d"))
        {
            if (!(jumping))
            {


                if (anim.GetCurrentAnimatorClipInfo(0).ToString() == "idle")
                {
                    anim.Play("run");
                }
                anim.SetBool("running", true);
                if (rb.velocity.x < speed)
                {
                    Vector2 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x);
                    transform.localScale = temp;
                    rb.AddForce(new Vector2(1, 0) * 1500 * Time.deltaTime);
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) < 2)
                {
                    Vector2 temp = transform.localScale;
                    temp.x = Mathf.Abs(temp.x);
                    transform.localScale = temp;
                }
            }
        }
        
        else if (Mathf.Abs(rb.velocity.x) < 2)
        {
            anim.SetBool("running", false);
            Vector2 temp = rb.velocity;
            temp.x = 0;
            //rb.velocity = temp;
        }
        if (Input.GetKeyDown("space") || Input.GetKeyDown("w"))
        {
            if (!jumping)
            {
                jumping = true;

                anim.Play("jump");
                anim.SetBool("jumping", true);
                Invoke("Jump", 0.2f);
            }
            
        }
        if (Input.GetKeyDown("f"))
        {
            Shoot();

        }
        if (rb.velocity.y < 0)
        {
            
            anim.SetBool("jumping", false);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy") //si el tag del objeto colisionado es "enemy"
        {
            Debug.Log("enemigo");// muestra en la consola el mensaje "enemigo"
        }
        jumping = false;
        anim.Play("fall");
        anim.SetBool("falling", false);
    }
}
