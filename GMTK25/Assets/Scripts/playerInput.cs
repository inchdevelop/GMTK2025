using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    Rigidbody2D rb;
    public float drag = 0.65f; //drag on dog so it slows
    public float defaultSpeed = 5; //default speed for dog
    public float speed = 0; //speed for dog
    public float dashSpeed = 20; //speed mult for dog
    public float speedFall = 0.2f; //speed fall off for dog

    public delegate void OnPlayerDash();
    public static event OnPlayerDash onPlayerDash;

    public delegate void OnPlayerPause();
    public static event OnPlayerPause onPlayerPause;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //drag code
        if (Mathf.Abs(rb.velocity.x) > 0 || Mathf.Abs(rb.velocity.y) > 0)
        {
            rb.AddForce(new Vector2(rb.velocity.x * -drag, rb.velocity.y * -drag));
        }

        //speed flat? code
        if (speed >= defaultSpeed)
        {
            speed -= speedFall * Time.deltaTime;
        }
        else
        {
            speed += speedFall * Time.deltaTime;
        }

        //input pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPlayerPause?.Invoke();
        }


        //input wasd and arrows
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
          rb.AddForce(Vector2.left * speed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * speed);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * speed);
        }

        //dash code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(doggyDash());
        }

        IEnumerator doggyDash()
        {
            if (GameManager.instance.CheckDash())
            {
                speed = dashSpeed;
                onPlayerDash?.Invoke();
            }
            yield return null;
        }
    }
}
