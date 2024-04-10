using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObjects : MonoBehaviour
{
    public float health;

    public void GetDamage(float damage)
    {
        health -= damage;
    }
}
