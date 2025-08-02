using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] float followSpeed;
    [SerializeField] float fleeSpeed;
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
        FLEE,
        ANIMATE
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
            case SheepMovement.ANIMATE:
                return;
            case SheepMovement.MOVE:
                if (moveTimer)
                    StartCoroutine(MoveTimer());
                MoveSheep();
                break;
            case SheepMovement.FOLLOW:
                SheepFollowTarget(followSpeed);
                break;
            case SheepMovement.FLEE:
                SheepFollowTarget(fleeSpeed);
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
      
            case SheepType.BROWN:
                SheepFollowTarget(GameObject.FindGameObjectWithTag("Player").transform.position, sheepSO.speed);
                break;
            default:
                SheepRandomMove();
                break;
        }
    }

    public void SheepRandomMove()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, sheepSO.speed * Time.deltaTime);
        RotateToNewPos(gameObject.transform.position, targetPos);
    }

    public void ChangeTargetPos()
    {
        Vector2 oldTargetPos = targetPos;

        GameObject[] sheepList = GameObject.FindGameObjectsWithTag("Sheep");

        //read type of sheep prob 
        bool canMove = false;
        bool done = false;
        while (!canMove)
        {
            canMove = true;
            targetPos = SheepManager.instance.GetRandomSpawnLocation();


            //if (sheepSO.type != SheepType.GOLD)
            //{
            //    float closestSheep = 5;
            //    Sheep theOne = null;
            //    for (int i = 0; i < sheepList.Length; i++)
            //    {
            //        if (sheepList[i].gameObject.GetComponent<Sheep>().sheepSO.type == SheepType.GOLD)
            //        {
            //            Vector2 tempPos = sheepList[i].gameObject.transform.position;
            //            float sheepDistance = Vector2.Distance(transform.position, tempPos);
            //            if (sheepDistance <= 5f && sheepDistance < closestSheep)
            //            {
            //                closestSheep = sheepDistance;
            //                theOne = sheepList[i].gameObject.GetComponent<Sheep>();
            //            }
            //        }
            //    }

            //    if(theOne != null)
            //    {
            //        targetPos = theOne.transform.position;
            //        done = true;
            //    }
            //}

            //if (sheepSO.type != SheepType.BLACK && !done)
            //{
            //    float closestSheep = 5;
            //    Sheep theOne = null;
            //    for (int i = 0; i < sheepList.Length; i++)
            //    {
            //        if (sheepList[i].gameObject.GetComponent<Sheep>().sheepSO.type == SheepType.BLACK)
            //        {
            //            Vector2 tempPos = sheepList[i].gameObject.transform.position;
            //            float sheepDistance = Vector2.Distance(transform.position, tempPos);
            //            if (sheepDistance <= 5f && sheepDistance < closestSheep)
            //            {
            //                closestSheep = sheepDistance;
            //                theOne = sheepList[i].gameObject.GetComponent<Sheep>();
            //            }
            //        }
            //    }

            //    if (theOne != null)
            //    {
            //        targetPos = 3f * (transform.position + ((transform.position - theOne.transform.position).normalized) );
            //    }
            //}

            if (!spawnBuffer.bounds.Contains(targetPos))
                canMove = false;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

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

    public void SheepFollowTarget()
    { 
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, sheepSO.speed * Time.deltaTime);
        RotateToNewPos(gameObject.transform.position, targetPos);
    }

    public void SheepFollowTarget(float theSpeed)
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, theSpeed * Time.deltaTime);
        RotateToNewPos(gameObject.transform.position, targetPos);
    }

    public void SheepFollowTarget(Vector3 target, float theSpeed)
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target, theSpeed * Time.deltaTime);

        RotateToNewPos(gameObject.transform.position, target);
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
        currentState = Sheep.SheepMovement.ANIMATE;
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
