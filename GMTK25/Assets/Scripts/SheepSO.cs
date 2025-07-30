using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheepData", menuName = "ScriptableObjects/SheepData", order = 1)]
public class SheepSO : ScriptableObject
{
    // Start is called before the first frame update

    [SerializeField] public Sprite sprite;
    [SerializeField] public int scoreValue;
    [SerializeField] public float moveInterval;
    [SerializeField] public Sheep.SheepType type;
}
