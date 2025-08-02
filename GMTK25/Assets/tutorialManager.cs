using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public bool sheepFate = false;//first sheep herded
    public bool bowlFate = false;//bowl watered

    public GameObject bowl;
    public GameObject TutBowl;
    public GameObject TutDash;

    public GameObject sheepbutMore;

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
        if (bowlFate)
        {
            dashTutor();
        }
    }

    void bowlTutor()
    { 
        bowl.SetActive(true);
        TutBowl.SetActive(true);
    }
    void dashTutor()
    {
        TutDash.SetActive(true);
    }

}
