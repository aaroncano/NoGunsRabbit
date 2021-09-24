using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private void Awake()
    {
        Events.playerHitEvent.AddListener(onHit); //se suscribe al evento playerhit.
    }

    private void onHit(hitEventData hitData)
    {
        gameObject.SetActive(false);
    }
}
