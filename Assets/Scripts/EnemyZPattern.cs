using UnityEngine;

public class EnemyZPattern : Enemy
{
    private float leftZPatternBoundary = -0.2f;
    private float rightZPatternBoundary = 0.8f;
    private bool makingDiagonal = false;
    private bool diagonalDone = false;

    Vector3 startPos;

    void OnEnable()
    {
        startPos = transform.position;
    }

    protected override void Movement()
    {
        // If the enemy has not hit the left boundary of the Z pattern and the diagonal movement has not been made, move left. Also if the diagonal movement finished
        if ((transform.position.x > leftZPatternBoundary && !makingDiagonal) || diagonalDone) 
            transform.Translate(Vector3.left * p_speed * Time.deltaTime, Space.World);

        // If the enemy hits the left boundary of the Z pattern, make the diagonal movement
        if ((transform.position.x <= leftZPatternBoundary || makingDiagonal) && !diagonalDone)
        {
            makingDiagonal = true;
            
            Vector3 diagonalFinalPosition = new Vector3(rightZPatternBoundary, startPos.y * -1, startPos.z);

            Vector3 lookDirection = (diagonalFinalPosition - transform.position).normalized;

            transform.position += lookDirection * p_speed * Time.deltaTime;
        }

        // If the enemy hits the right boundary of the Z pattern, mark the diagonal movement as finished
        if (transform.position.x >= rightZPatternBoundary && makingDiagonal)
        {
            makingDiagonal = false;
            diagonalDone = true;
        }

    }
}
