using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotHitscan : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask hitLayers;
    private float damage = 5f;

    [SerializeField] private float fireRate = 0.5f;
    private bool waitForShot = false;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }
    private void Update()
    {

        if (inputManager.PlayerShot() && !waitForShot)
        {
            StartCoroutine(ShotWait());
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Vector3 shootDirection = cameraTransform.forward;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance, hitLayers))
        {
            if (hit.transform.GetComponent<DamageableObjects>() != null)
            {
                //hit.transform.GetComponent<DamageableObjects>().GetDamage(damage);
            }
        }
    }
    private IEnumerator ShotWait()
    {
        waitForShot = true;
        yield return new WaitForSeconds(fireRate);
        waitForShot = false;
    }
}
