using UnityEngine;

public class EnemySinusoidal : Enemy
{
    [SerializeField] float amplitude = 1f;
    [SerializeField] float frequency = 1f; 
    private Vector3 startPos;

    void OnEnable()
    {
        startPos = transform.position;
    }

    protected override void UpdateThreatType()
    {
        ThreatType =  GameManager.ThreatTypes.Sinusoidal;
    }

    protected override void Movement()
    {
        transform.position += Vector3.left * p_speed * Time.deltaTime;

        // Apply the sine wave offset
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        // Adapt movement to curve
        transform.position = new Vector3(
            transform.position.x,
            startPos.y + yOffset,
            startPos.z
        );
    }
}
