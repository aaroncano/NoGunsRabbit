using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float timeToDeath = 0.5f; //tiempo que toma en destruirse una vez lo han impactado.
    private bool hasBeenHit;

    private void Awake()
    {
        hasBeenHit = false;
        Events.enemyHitEvent.AddListener(onHit);
    }

    private void onHit(hitEventData hitData)
    {
        if(hitData.target == gameObject)
        {
            if (hasBeenHit) return;
            else
            {
                StartCoroutine(deadCoroutine());
                hasBeenHit = true;
            }
        }
    }

    private IEnumerator deadCoroutine()
    {
        yield return new WaitForSeconds(timeToDeath);
        gameObject.SetActive(false);
    }
}
