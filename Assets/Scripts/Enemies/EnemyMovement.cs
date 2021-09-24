using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private Transform wallCheck = null;
    [SerializeField] private LayerMask ground = 1;

    private bool patrol;
    private bool turn_Ground;
    private bool turn_Wall;

    private Rigidbody2D rb;
    private EnemyShot enemyShot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyShot = GetComponent<EnemyShot>();
        patrol = true;
        turn_Wall = false;
        turn_Ground = false;
    }

    private void FixedUpdate()
    {
        if (patrol)
        {
            move();
            turn_Ground = Physics2D.OverlapCircle(groundCheck.position, 0.02f, ground);
            turn_Wall = Physics2D.OverlapCircle(wallCheck.position, 0.02f, ground);
        }
    }

    private void move()
    {
        if(turn_Wall || turn_Ground)
        {
            flip();
            movementSpeed *= -1;
        }

        rb.velocity = new Vector2(movementSpeed * 10 * Time.fixedDeltaTime, rb.velocity.y);
    }
    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        enemyShot.changeDetectPlayerRange();
        turn_Ground = false;
        turn_Wall = false;
    }
}
