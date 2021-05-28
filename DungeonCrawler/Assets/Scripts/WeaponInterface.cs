using UnityEngine;

public interface Weapon
{
    public void Start();
    public void Shoot(Transform shootingPoint);

    public float getSpeed();

    public float getFireRate();
}
