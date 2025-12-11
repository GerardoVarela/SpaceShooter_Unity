using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }
}
