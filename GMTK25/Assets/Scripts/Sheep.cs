using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] SheepSO sheepSO;
    public enum SheepType
    {
        WHITE,
        BLACK,
        GOLD,
        NUM_SHEEP
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sheepSO.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
