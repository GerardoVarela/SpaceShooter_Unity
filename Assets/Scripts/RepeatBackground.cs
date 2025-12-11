using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position; // Establish the default starting position 
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2; // Set repeat width to half of the background
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // If background moves left by its repeat width, move it back to start position
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
