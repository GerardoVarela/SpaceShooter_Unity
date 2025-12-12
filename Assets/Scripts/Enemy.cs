using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] Color color = Color.white;
    public float p_speed
    {
        get { return speed; }
    }

    private AudioSource cameraAudioSource;
    public AudioClip explodeSound;

    public GameObject explosion;
    public GameManager gameManager;

    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateThreatType();
    }

    void Update()
    {
        Movement();
    }
    
    protected virtual void Movement()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    private GameManager.ThreatTypes m_ThreatType;
    public GameManager.ThreatTypes ThreatType
    {
        get { return m_ThreatType; }
        set { m_ThreatType = value; }
    }
    protected abstract void UpdateThreatType();

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Bullet") || collider.CompareTag("Player"))
        {
            cameraAudioSource.PlayOneShot(explodeSound, 1.5f);

            GameObject explosionPrefab = Instantiate(explosion, transform.position, Quaternion.identity);
            SpriteRenderer explosionSpriteRenderer = explosionPrefab.GetComponent<SpriteRenderer>();
            explosionSpriteRenderer.color = color;

            Destroy(gameObject);
            if (!collider.CompareTag("Player")) {
                Destroy(collider.gameObject);
                gameManager.ThreatDestroyed(m_ThreatType);   
            }
        }
    }
}
