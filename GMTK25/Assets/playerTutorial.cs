using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class playerTutorial : MonoBehaviour
{

    Vector3 pastPos;
    public TMP_Text top;
    public TMP_Text bottom;
    float color = 1f;

    // Start is called before the first frame update
    void Start()
    {
        pastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pastPos) > 2f)
        {
            StartCoroutine(fadeAway());
        }
    }

    IEnumerator fadeAway()
    {
        top.GetComponent<Rigidbody2D>().gravityScale = 0;
        top.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10,10));
        bottom.GetComponent<Rigidbody2D>().gravityScale = 0;
        bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(650, -500));
        yield return null;
    }
}
