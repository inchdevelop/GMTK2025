using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    public float highScore;
    public static ScoreHolder instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
