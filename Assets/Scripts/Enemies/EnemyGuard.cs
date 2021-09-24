using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuard : MonoBehaviour
{
    [SerializeField] private float flippingTime = 5f;

    private float timeToFlip;
    private EnemyShot enemyShot;

    private void Awake()
    {
        timeToFlip = flippingTime;
        enemyShot = GetComponent<EnemyShot>();
    }

    private void Update()
    {
        if(timeToFlip <= 0f)
        {
            flip();
            timeToFlip = flippingTime;
        }
        else
        {
            timeToFlip -= Time.deltaTime;
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        enemyShot.changeDetectPlayerRange();
    }
}
 