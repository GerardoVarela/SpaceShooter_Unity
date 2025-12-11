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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(explodeSound, 1.5f);

            GameObject explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
            SpriteRenderer explosionSpriteRenderer = explosionPrefab.GetComponent<SpriteRenderer>();
            explosionSpriteRenderer.color = color;

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
