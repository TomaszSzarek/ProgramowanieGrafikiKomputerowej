using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float destroyDelay = 10f;
    public float speed = 10f;
    [SerializeField]private float damage = 5f;
    private Rigidbody rb;

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
        Destroy(gameObject);
    }
}
