using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 
    public float rotationSpeed = 5f; 

    void Update()
    {
       
        transform.position = player.position + offset;

       
        transform.LookAt(player);

        if (Input.GetKey(KeyCode.Q)) 
        {
            transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) 
        {
            transform.RotateAround(player.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}
