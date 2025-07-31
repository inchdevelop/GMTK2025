using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class lassoDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        KeyCode slast;
        KeyCode last;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Lasso")
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.up, Color.white, 5f);
            Debug.Log("drawing");
        }
    }
}
