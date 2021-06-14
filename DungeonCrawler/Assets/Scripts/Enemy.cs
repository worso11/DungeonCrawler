using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

public interface Enemy
{

    public void Start();

    public void Update();

    public void takeDamage(float dmg);

    public void OnCollisionEnter2D(Collision2D other);

    public IEnumerator Knockback(float duration, float power, Transform obj);

    public void OnBecameVisible();

    public void OnBecameInvisible();

    public void OnDestroy();
}
