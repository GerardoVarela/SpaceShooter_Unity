using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequency = 1f; 
    Vector3 startPos;

    private AudioSource audioSource;
    public AudioClip explodeSound;

    void Start()
    {
        startPos = transform.position;
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Update()
    {
        SinusoidalMovement();
    }
    
    void SinusoidalMovement()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Apply the sine wave offset
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Combine linear movement with sine
        transform.position = new Vector3(
            transform.position.x,
            startPos.y + yOffset,
            startPos.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(explodeSound, 1.5f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
