using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dogBowlTutorial : MonoBehaviour
{
    Vector3 pastPos;
    public TMP_Text top;
    public TMP_Text bottom;
    public bool beenUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        pastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator fadeAway()
    {
        top.GetComponent<Rigidbody2D>().AddForce(new Vector2(10000, 0) * Time.deltaTime * 10);
        bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10000, 0) * Time.deltaTime * 10);
        yield return new WaitForSeconds(3f);

        GameObject.FindGameObjectWithTag("tutor").gameObject.GetComponent<tutorialManager>().bowlFate = true;

        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(fadeAway());
    }
}
