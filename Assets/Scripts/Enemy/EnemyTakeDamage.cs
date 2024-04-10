using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private DamageableObjects damageableObjects;
    private Renderer enemyRenderer;
    private void Awake()
    {
        damageableObjects = GetComponent<DamageableObjects>();
        enemyRenderer = GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        if (damageableObjects.health <= 20)
        {
            enemyRenderer.material.color = Color.red;
            if (damageableObjects.health <= 0)
            {
                StartCoroutine(DestroyThisObject());
            }
                
        }   
    }
    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
