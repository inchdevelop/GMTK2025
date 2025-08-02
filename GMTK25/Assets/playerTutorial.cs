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
        top.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1000,0) * Time.deltaTime * 10);
        bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 0) * Time.deltaTime * 10);
        yield return null;
    }
}
