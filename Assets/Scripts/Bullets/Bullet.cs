using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour
{
    private float destroyDelay = 10f;
    public float speed = 10f;
    [SerializeField]private float damage = 5f;
    private Rigidbody rb;

    public GameObject ParticlePrefab;//particlesy
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetDirection(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, destroyDelay);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamageableObjects>() != null)
        {
            collision.gameObject.GetComponent<DamageableObjects>().GetDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.GetDamage(damage);
            Destroy(gameObject);
        }

        //particless nie dziala cos
        //GameObject particle = Instantiate(ParticlePrefab, rb.velocity, Quaternion.identity);
        Destroy(gameObject);
    }
}
