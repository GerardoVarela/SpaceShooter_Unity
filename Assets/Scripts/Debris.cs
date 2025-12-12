using UnityEditor.Animations;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float minRotationSpeed = 25f;
    [SerializeField] float maxRotationSpeed = 45f;
    private float rotationSpeed;
    private int orientation;

    private AudioSource cameraAudioSource;
    public AudioClip explodeSound;

    private Animator debrisAnimator;


    public GameObject explosion;


    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        debrisAnimator = GetComponent<Animator>();

        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        orientation = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    void Update()
    {
        MoveLeft();
        RotateDebris();
    }
    
    void MoveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }

    void RotateDebris()
    {
        transform.Rotate(0f, 0f, rotationSpeed * orientation * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Bullet") || collider.CompareTag("Player"))
        {
            cameraAudioSource.PlayOneShot(explodeSound, 1.5f);

            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
            if (!collider.CompareTag("Player")) Destroy(collider.gameObject);
        }
    }
}
