using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class lassoDetect : MonoBehaviour
{
    public delegate void OnSheepCollected(int score);
    public static event OnSheepCollected onSheepCollected;

    public delegate void OnLassoCollect();
    public static event OnLassoCollect onLassoCollect;
    int sheepCount = 0;
    public bool tutorial = false;

    [SerializeField] List<GameObject> sheepList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(killCode());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Sheep")
        {
            sheepList.Add(collider.gameObject);
        }
    }

    IEnumerator killCode()
    {
        yield return new WaitForSeconds(.25f);
        if (tutorial)
        {
            Debug.Log("SHEEP");
            GameObject.FindGameObjectWithTag("tutor").gameObject.GetComponent<tutorialManager>().sheepFate = true;
            GameObject.FindGameObjectWithTag("tutor").gameObject.GetComponent<tutorialManager>().sheepCounter++;
            tutorSheep();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(sheepCount);
            CollectSheep();
            Destroy(gameObject);
        }
    }

    void CollectSheep()
    {
        if (sheepList.Count <= 0)
            return;
        //collects sheep
        for (int i = 0; i < sheepList.Count; i++)
        {
            if (i == 0)
            {
                onLassoCollect?.Invoke();
                Debug.Log("starting combo");
            }
            onSheepCollected?.Invoke(sheepList[i].GetComponent<Sheep>().sheepSO.scoreValue);
            
        }
        //destroys sheep 
        for(int i = 0; i < sheepList.Count; i++)
        {
            SheepManager.instance.DestroySheep(sheepList[i]);
            sheepList.Remove(sheepList[i]);
        }
        sheepList.Clear();
    }
    void tutorSheep()
    {
        if (sheepList.Count <= 0)
            return;
        //destroys sheep 
        for (int i = 0; i < sheepList.Count; i++)
        {
            SheepManager.instance.DestroySheep(sheepList[i]);
            sheepList.Remove(sheepList[i]);
        }
        sheepList.Clear();
    }
}
