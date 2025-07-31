using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class playerLineCreation : MonoBehaviour
{

    public GameObject lasso;
    public GameObject player;
    private SpriteShapeController sprite;
    private Spline spline;
    private PolygonCollider2D spritetri;
    public float timer;
    public float timer2;
    public float timeadj;

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteShapeController>();
        spline = sprite.spline;
        spritetri = GetComponent<PolygonCollider2D>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        timer2 += Time.deltaTime;

        if (timer2 >= timeadj)
        {
            //spritetri.points.SetValue(player.gameObject.transform.position, count);
            count++;
            GameObject newball = Instantiate(lasso);
            newball.transform.position = player.transform.position;
            Destroy(newball,timer);
            //spline.InsertPointAt(count, player.transform.position);
            //StartCoroutine(doggyLine());
            timer2 = 0;
        }

        //if (count == 2)
        //{
        //    spline.SetPosition(0, new Vector2(0, 50));
        //    spline.SetPosition(1, new Vector2(0, 51));
        //}
    }

    IEnumerator doggyLine()
    {
        //int beforecount = spline.GetPointCount();
        //yield return new WaitForSeconds(3);
        //Debug.Log("goal" + beforecount);
        //for (int i = 0; i < beforecount; i++)
        //{
        //    Debug.Log("rahh" + i);
        //    spline.RemovePointAt(i);
        //    //spline.SetPosition(i, player.transform.position);
        //}
        //count = 0;
        yield return new WaitForSeconds(timer);

        spline.RemovePointAt(0);

        count = spline.GetPointCount() - 1;

        if (count == 1)
        {
            spline.SetPosition(0, player.transform.position);
            spline.SetPosition(1, player.transform.position * 0.9f);
        }

    }

    IEnumerator line()
    {
        yield return new WaitForSeconds(0.5f);

        count = spline.GetPointCount() - 1;

        yield return null;
    }
}
