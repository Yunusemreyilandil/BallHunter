using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Karakterin transformu
    public Vector3 offset; // Kamera ile karakter aras�ndaki mesafe
    public float rotationSpeed = 5f; // Kameran�n d�n�� h�z�

    void Update()
    {
        // Kameray�, karakterin �zerine belirli bir mesafede sabit tut
        transform.position = player.position + offset;

        // Kameran�n karakteri takip etmesini sa�la
        transform.LookAt(player);

        // Kameray� d�nd�rme (top-down g�r�n�mde hafif bir yatay hareket eklemek i�in)
        if (Input.GetKey(KeyCode.Q)) // Q tu�una basarak kameray� sola �evir
        {
            transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) // E tu�una basarak kameray� sa�a �evir
        {
            transform.RotateAround(player.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}
