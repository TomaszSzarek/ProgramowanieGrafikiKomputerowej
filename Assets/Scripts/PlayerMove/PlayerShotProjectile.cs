using System.Collections;
using UnityEngine;

public class PlayerShotProjectile : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate=0.5f;
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
            Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward * 2f;
            Vector3 shootDirection = cameraTransform.forward;
            GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            newBullet.GetComponent<Bullet>().SetDirection(shootDirection);
        }
    }
    private IEnumerator ShotWait()
    {
        waitForShot = true;
        yield return new WaitForSeconds(fireRate);  
        waitForShot = false;
    }
}
