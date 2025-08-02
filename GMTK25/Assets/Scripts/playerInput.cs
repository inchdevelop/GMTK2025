using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    Rigidbody2D rb;
    public float drag = 0.65f; //drag on dog so it slows
    public float defaultDrag = 0.45f; //speed mult for dog
    public float dragFall = 0.2f; //speed fall off for dog
    public float dragMax = 20; //speed mult for dog
    public float defaultSpeed = 5; //default speed for dog
    public float rotationSpeed = 250; //default speed for dog
    public float speed = 0; //speed for dog
    public float dashSpeed = 20; //speed mult for dog
    public float speedFall = 0.2f; //speed fall off for dog

    public DogStates currentState;

    public delegate void OnPlayerDash();
    public static event OnPlayerDash onPlayerDash;

    public delegate void OnPlayerPause();
    public static event OnPlayerPause onPlayerPause;

    public enum DogStates
    {
        RUN,
        DASH,
        NUM_STATES
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = defaultSpeed;
        drag = defaultDrag;
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
        if (speed > defaultSpeed)
        {
            speed -= speedFall * Time.deltaTime;

        }
        else
        {
            speed += speedFall * Time.deltaTime;
        }
        if (drag > defaultDrag)
        {
            drag -= dragFall * Time.deltaTime;

        }
        else
        {
            drag += dragFall * Time.deltaTime;
        }
        if (speed >= dashSpeed*0.9f)
        {
            currentState = DogStates.DASH;
        }
        else
        {
            currentState = DogStates.RUN;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.right), rotationSpeed * Time.deltaTime);
            //transform.Rotate(Vector2.right);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * speed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.left), rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * speed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.down), rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * speed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, Vector2.up), rotationSpeed * Time.deltaTime);
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
                drag = dragMax;
                onPlayerDash?.Invoke();
            }
            yield return null;
        }
    }
}
