using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    private DamageableObjects damageableObjects;
    private Renderer enemyRenderer;
<<<<<<< Updated upstream
=======
    public Material bloodMaterial; 
    float actual = 100;

>>>>>>> Stashed changes
    private void Awake()
    {
        damageableObjects = GetComponent<DamageableObjects>();
        enemyRenderer = GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
<<<<<<< Updated upstream
=======
        if (damageableObjects.health != actual)
        {
            Vector3 spawnPosition = damageableObjects.hitPositionOb;
            CreateBloodEffect(spawnPosition);
            actual = damageableObjects.health;
        }

>>>>>>> Stashed changes
        if (damageableObjects.health <= 20)
        {
            enemyRenderer.material.color = Color.red;
            if (damageableObjects.health <= 0)
            {
                StartCoroutine(DestroyThisObject());
            }
                
        }   
    }

    private void CreateBloodEffect(Vector3 position)
    {
        // Tworzenie tymczasowego GameObject z prymitywem, aby uzyskaæ mesh.
        GameObject tempPrimitive = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Mesh quadMesh = tempPrimitive.GetComponent<MeshFilter>().mesh;

        // Teraz mo¿na zbudowaæ efekt krwi.
        GameObject bloodEffect = new GameObject("BloodEffect");
        Renderer renderer = bloodEffect.AddComponent<MeshRenderer>();
        renderer.material = bloodMaterial;
        renderer.material.color = Color.red;
        MeshFilter meshFilter = bloodEffect.AddComponent<MeshFilter>();
        meshFilter.mesh = quadMesh;

        // Ustawienie pozycji i skali efektu krwi.
        bloodEffect.transform.position = position;
        bloodEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f); // Dopasuj rozmiar efektu

        // Niszczenie tymczasowego GameObject oraz efektu krwi po pewnym czasie.
        Destroy(tempPrimitive);  // Usuñ tymczasowy GameObject
        Destroy(bloodEffect, 5f); // Usuñ efekt krwi po 5 sekundach
    }
    private IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
