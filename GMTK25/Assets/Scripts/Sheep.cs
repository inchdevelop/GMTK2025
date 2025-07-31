using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] public BoxCollider2D spawnBuffer;
    [SerializeField] SheepSO sheepSO;

    [SerializeField] Vector2 targetPos;
    bool moveTimer = true;
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
        ChangeTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        MoveSheep();
        if(moveTimer)
        {
            StartCoroutine(MoveTimer());
        }
    }

    public IEnumerator MoveTimer()
    {
        moveTimer = false;
        ChangeTargetPos();
        yield return new WaitForSeconds(sheepSO.moveInterval);
        moveTimer = true;
    }

    public void MoveSheep()
    {
        Debug.Log("moving" + gameObject.name);
        switch(sheepSO.type)
        {
            case SheepType.WHITE:
                MoveWhiteSheep();
                break;
            case SheepType.BLACK:
                MoveBlackSheep();
                break;
            case SheepType.GOLD:
                MoveGoldSheep();
                break;
        }
    }

    public void MoveWhiteSheep()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, sheepSO.speed);
    }

    void ChangeTargetPos()
    {
        bool canMove = false;
        while (!canMove)
        {
            canMove = true;
            targetPos = SheepManager.instance.GetRandomSpawnLocation();
            if (!spawnBuffer.bounds.Contains(targetPos))
                canMove = false;
        }
    }

    public void MoveBlackSheep()
    {

    }
    
    public void MoveGoldSheep()
    {

    }
}
