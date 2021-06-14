using UnityEngine;

public interface Weapon
{
    public void PrepareWeapon(int power);
    public void Shoot(Transform shootingPoint);

    public float getSpeed();

    public float getFireRate();

    public void OnTriggerEnter2D(Collider2D other);
}
