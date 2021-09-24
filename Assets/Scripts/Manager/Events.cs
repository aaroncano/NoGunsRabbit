using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnHitEvent : UnityEvent<hitEventData> { }

public static class Events
{
    public static OnHitEvent playerHitEvent = new OnHitEvent();
    public static OnHitEvent enemyHitEvent = new OnHitEvent();
}

public class hitEventData
{
    public GameObject shooter;
    public GameObject target;

    public hitEventData(GameObject shooter, GameObject target)
    {
        this.shooter = shooter;
        this.target = target;
    }
}
