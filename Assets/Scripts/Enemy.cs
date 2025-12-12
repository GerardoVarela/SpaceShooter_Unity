using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Color color = Color.white;
    public float p_speed
    {
        get { return speed; }
    }

    private AudioSource audioSource;
    public AudioClip explodeSound;

    public GameObject explosion;

    void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        
    }

    void Update()
    {
        Movement();
    }
    
    protected virtual void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Bullet") || collision.CompareTag("Player"))
    //     {
    //         audioSource.PlayOneShot(explodeSound, 1.5f);

    //         GameObject explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
    //         SpriteRenderer explosionSpriteRenderer = explosionPrefab.GetComponent<SpriteRenderer>();
    //         explosionSpriteRenderer.color = color;

    //         Destroy(gameObject);
    //         if (!collision.CompareTag("Player")) Destroy(collision.gameObject);
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Bullet") || collider.CompareTag("Player"))
        {
            audioSource.PlayOneShot(explodeSound, 1.5f);

            GameObject explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
            SpriteRenderer explosionSpriteRenderer = explosionPrefab.GetComponent<SpriteRenderer>();
            explosionSpriteRenderer.color = color;

            Destroy(gameObject);
            if (!collider.CompareTag("Player")) Destroy(collider.gameObject);
        }
    }
}
