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

    public int maxPoints = 30;
    public float minDist = 0.75f;
    public float pointCheck = 0.5f;
    public List<Vector2> pointList;
    Vector3 pastPos;
    public List<Vector2> tempList;

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

            pastPos = player.transform.position;
        }

        if (pointList.Count >= 4) 
        {
            checkConnect();
        }


        if (pointList.Count > maxPoints)
        {
            pointList.RemoveAt(0);
        }
    }

    void createLasso()
    {
        GameObject newShape = Instantiate(lasso);
        newShape.GetComponent<PolygonCollider2D>().enabled = true;
        newShape.GetComponent<PolygonCollider2D>().SetPath(0, tempList);

        pointList.Clear();
        tempList.Clear();
    }

    void checkConnect()
    {        
        for (int i = 0;i < pointList.Count-1;i++)
        { 
            for(int j = 0; j < pointList.Count-1; j++)
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
