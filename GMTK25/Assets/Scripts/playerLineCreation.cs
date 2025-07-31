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

    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteShapeController>();
        spline = sprite.spline;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            count++;
            //GameObject newball = Instantiate(lasso);
            //newball.transform.position = player.transform.position;
            //Destroy(newball,5f);
            spline.InsertPointAt(count, player.transform.position);
            StartCoroutine(doggyLine());
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
        yield return new WaitForSeconds(3f);

        spline.RemovePointAt(0);

        count = spline.GetPointCount() - 1;

        yield return null;
    }

    IEnumerator line()
    {
        yield return new WaitForSeconds(0.5f);

        count = spline.GetPointCount() - 1;

        yield return null;
    }
}
