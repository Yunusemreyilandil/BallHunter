using UnityEngine;

public class FleeingBall : MonoBehaviour
{
    public float triggerDistance = 10f;
    public float fleeSpeed = 3f;

    private Transform player;
    private bool isFleeing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isFleeing && distance < triggerDistance)
        {
            isFleeing = true;
        }

        if (isFleeing)
        {
            Vector3 direction = transform.position - player.position;
            direction.y = 0f; 

            if (direction != Vector3.zero)
            {
                transform.position += direction.normalized * fleeSpeed * Time.deltaTime;
            }
        }
    }
}
