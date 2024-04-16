using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float destroyDelay = 10f;
    public float speed = 10f;
    [SerializeField]private float damage = 5f;
    private Rigidbody rb;

    ContactPoint contact;
    Vector3 hitPosition;

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
            contact = collision.contacts[0];
            hitPosition = contact.point;
            collision.gameObject.GetComponent<DamageableObjects>().GetDamage(damage, hitPosition);
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
