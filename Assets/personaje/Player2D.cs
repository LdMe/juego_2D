using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed,boostSpeed,baseSpeed;
    float z;
    public bool boost;
    public GameObject shotPrefab;

    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
        rb = GetComponent<Rigidbody2D>();
    }
    void Shoot()
    {
        GameObject shot = Instantiate(shotPrefab);// crear objeto de tipo shotPrefab
        shot.transform.position = transform.position;
        shot.transform.rotation = transform.rotation;
        shot.GetComponent<Rigidbody2D>().velocity = transform.right * 10;// damos velocidad al shot
        Destroy(shot, 5);// destruimos el objeto en 5 segundos
    }
    // Update is called once per frame
    void Update()
    {
        if (boost)
        {
            speed = boostSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        if (Input.GetKey("a"))
        {
            rb.velocity = transform.up * speed;
        }
        else if (Input.GetKey("d"))
        {
            rb.velocity = transform.up * -speed;
        }
        else if (Input.GetKey("s"))
        {
            rb.velocity = transform.right * -speed;
        }
        else if (Input.GetKey("w"))
        {
            rb.velocity = transform.right * speed;
        }
        else if (Input.GetMouseButton(0))
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
             rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }
}
