using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public bool sheepFate = false;

    public GameObject bowl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepFate)
        {
            bowlTutor();
        }
    }

    void bowlTutor()
    { 
        bowl.SetActive(true);
    }

}
