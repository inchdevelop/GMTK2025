using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.ParticleSystem;

public class playercreateLassoCreation : MonoBehaviour
{

    public GameObject lasso;
    public GameObject player;
    public GameObject line;

    public int maxPoints = 100;
    public float minDist = 0.25f;
    public float pointCheck = 0.2f;
    public List<Vector2> pointList;
    Vector3 pastPos;
    public List<Vector2> tempList;
    public List<GameObject> lineList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, pastPos) > minDist)
        {
            pointList.Add(player.transform.position);

            GameObject newLine = Instantiate(line);
            newLine.transform.position = player.transform.position;
            newLine.transform.rotation = player.transform.rotation;
            lineList.Add(newLine);

            pastPos = player.transform.position;
        }

        if (pointList.Count >= 4)
        {
            checkConnect();
        }


        if (pointList.Count > maxPoints)
        {
            pointList.RemoveAt(0);
            Destroy(lineList[0]);
            lineList.RemoveAt(0);
        }

        for (int i = 0; i < lineList.Count; i++)
        {
            lineList[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)i / lineList.Count);
        }
    }

    void createLasso()
    {
        GameObject newShape = Instantiate(lasso);
        newShape.GetComponent<PolygonCollider2D>().enabled = true;
        newShape.GetComponent<PolygonCollider2D>().SetPath(0, tempList);

        pointList.Clear();
        tempList.Clear();
        for (int i = 0; i < lineList.Count; i++)
        {

            Destroy(lineList[i]);
        }
        lineList.Clear();
    }

    void checkConnect()
    {
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            for (int j = 0; j < pointList.Count - 1; j++)
            {
                if (Vector2.Distance(pointList[i], pointList[j]) < pointCheck && i != j)
                {
                    for (int k = i; k <= j; k++)
                    {
                        tempList.Add(pointList[k]);
                    }
                    createLasso();
                }
            }
        }
    }
}
