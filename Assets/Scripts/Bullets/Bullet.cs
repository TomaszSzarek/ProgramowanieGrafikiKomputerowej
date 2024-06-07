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
    ParticleSystem particleSys;
    GameObject particle;
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
        //particless nie dziala cos
        particle = Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
        particleSys = particle.GetComponent<ParticleSystem>();
        particleSys.Play();

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
    }

    void Update()
    {
        if (particleSys != null && !particleSys.IsAlive())
        {
            Destroy(particle);
            Destroy(gameObject);
        }
    }

}
