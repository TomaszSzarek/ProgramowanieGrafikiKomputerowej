using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private DamageableObjects damageableObjects;
    private Renderer enemyRenderer;
    //public Material bloodMaterial;
    float actual = 100;
    private void Awake()
    {
        damageableObjects = GetComponent<DamageableObjects>();
        enemyRenderer = GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        /*if (damageableObjects.health != actual) { 
            bloodMaterial.SetVector("_ClickPosition", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
            bloodMaterial.SetFloat("_Radius", 0.1f);
            actual = damageableObjects.health;
        }*/

        //if (damageableObjects.health <= 20)
        //{
            //enemyRenderer.material.color = Color.red;
            if (damageableObjects.health <= 0)
            {
            GetComponent<DisintegrationController>().StartDisintegration();
                //StartCoroutine(DestroyThisObject());
            }
                
        //}   
    }
    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
