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
    [SerializeField] public SheepSO sheepSO;
    [SerializeField] public Animator sheepAnimator;
    [SerializeField] GameObject sprite;

    [SerializeField] public Vector3 targetPos;
    [SerializeField] Quaternion targetRot;
    [SerializeField] float rotSpeed;
    bool moveTimer = true;

    [SerializeField] public SheepMovement currentState = SheepMovement.MOVE;
    public enum SheepType
    {
        WHITE,
        BLACK,
        GOLD,
        FAT,
        PINK,
        RED,
        BROWN,
        BLUE,
        NUM_SHEEP
    }

    public enum SheepMovement
    {
        MOVE,
        FOLLOW,
        FLEE
    }

    public delegate void OnSheepCollide();
    public static event OnSheepCollide onSheepCollide;

    public delegate void OnSheepKnockUp(GameObject sheep);
    public static event OnSheepKnockUp onSheepKnockUp;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sheepSO.sprite;
        ChangeTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case SheepMovement.MOVE:
                if (moveTimer)
                    StartCoroutine(MoveTimer());
                MoveSheep();
                break;
            case SheepMovement.FOLLOW:
                MoveBrownSheep();
                break;
            case SheepMovement.FLEE:
                MoveBrownSheep();
                break;
        };
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
        //Debug.Log("moving" + gameObject.name);
        switch(sheepSO.type)
        {
            case SheepType.WHITE:
                MoveWhiteSheep();
                break;
            case SheepType.BLACK:
                MoveWhiteSheep();
                break;
            case SheepType.GOLD:
                MoveWhiteSheep();
                break;
            case SheepType.FAT:
                MoveWhiteSheep();
                break;
            case SheepType.PINK:
                MoveWhiteSheep();
                break;
            case SheepType.RED:
                MoveWhiteSheep();
                break;
            case SheepType.BROWN:
                MoveBrownSheep();
                break;
            case SheepType.BLUE:
                MoveWhiteSheep();
                break;
        }
    }

    public void MoveWhiteSheep()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, sheepSO.speed);
        RotateToNewPos(gameObject.transform.position, targetPos);
    }

    public void ChangeTargetPos()
    {
        Vector2 oldTargetPos = targetPos;

        //read type of sheep prob 
        bool canMove = false;
        while (!canMove)
        {
            canMove = true;
            targetPos = SheepManager.instance.GetRandomSpawnLocation();
            if (!spawnBuffer.bounds.Contains(targetPos))
                canMove = false;
        }
        
    }

    void RotateToNewPos(Vector3 oldPos, Vector3 newPos)
    {
        Vector2 targetDirection = (transform.position - newPos).normalized;

        float angle = (Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg) - 90f;
        Vector3 newAngle = new Vector3(0, 0, angle);

        gameObject.transform.eulerAngles = newAngle;
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, targetPos);
    }

    public void MoveBrownSheep()
    { 
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, player.transform.position, sheepSO.speed * Time.deltaTime);
        
        RotateToNewPos(gameObject.transform.position, player.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        playerInput thePlayer = collision.gameObject.GetComponent<playerInput>();

        if (thePlayer.currentState == playerInput.DogStates.RUN)
        {
            onSheepCollide?.Invoke();
            SheepCollide();
        }
        else
        {
            onSheepKnockUp?.Invoke(gameObject);
            SheepKnockUp();
        }

    }

    public void SheepCollide()
    {
        
    }

    public void SheepKnockUp()
    {
        sheepAnimator.Play("SheepKnockUp");
    }

    public void SheepDestroy()
    {
        Destroy(gameObject);
    }

    public void RedSheepExplode()
    {
        if (sheepSO.type != SheepType.RED)
            return;
        GetComponent<RedSheepZone>().Explosion();
    }
}
