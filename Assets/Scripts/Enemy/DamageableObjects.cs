using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObjects : MonoBehaviour
{
    public float health;
    public Vector3 hitPositionOb;

    public void GetDamage(float damage,Vector3 hitPosition)
    {
        health -= damage;
        hitPositionOb = hitPosition;
    }
}
