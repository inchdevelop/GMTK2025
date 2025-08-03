using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    public bool sheepFate = false;//first sheep herded
    public bool bowlFate = false;//bowl watered
    public bool dashFate = false;//dashing watered
    public bool once = true;//dashing watered
    public bool twice = true;//dashing watered

    public GameObject bowl;
    public GameObject TutBowl;
    public GameObject TutDash;

    public GameObject fadeTut;
    public RawImage fadeinTut;

    public TMP_Text top;
    public TMP_Text bottom;

    public GameObject sheepbutMore;
    public GameObject sheepbutMorespawn1;
    public GameObject sheepbutMorespawn2;

    public int sheepCounter = 0;

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
        if (bowlFate && sheepFate && once)
        {
            StartCoroutine(sheepAway());
            once = false;
        }
        if (bowlFate && sheepFate && GameObject.FindGameObjectWithTag("Player").GetComponent<playerInput>().currentState == playerInput.DogStates.DASH)
        {
            StartCoroutine(fadeAway());
        }
        if (sheepCounter >= 8 && twice == true)
        {
            finalTutor();
            twice= false;
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
    IEnumerator fadeAway()
    {
        top.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000) * Time.deltaTime * 100);
        bottom.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1000) * Time.deltaTime * 100);
        yield return null;
    }
    IEnumerator sheepAway()
    {
        yield return new WaitForSeconds(2f);
        sheepbutMorespawn1.GetComponent<BoxCollider2D>().size = new Vector2(0.8f,0.59f);
        sheepbutMorespawn2.GetComponent<BoxCollider2D>().size = new Vector2(0.45f, 0.84f);
        sheepbutMore.GetComponent<SheepManager>().SpawnNumSheep(7);
        yield return null;
    }
    void finalTutor()
    {
        fadeTut.SetActive(true);
        StartCoroutine(fadeIn());
    }
    IEnumerator fadeIn()
    {
        for (int i = 0; i <= 255; i++)
        {
            fadeinTut.color = new Color(0, 0, 0, (float)i/255.0f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("PlayScene");

        yield return null;
    }
}
