using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Karakterin transformu
    public Vector3 offset; // Kamera ile karakter arasýndaki mesafe
    public float rotationSpeed = 5f; // Kameranýn dönüþ hýzý

    void Update()
    {
        // Kamerayý, karakterin üzerine belirli bir mesafede sabit tut
        transform.position = player.position + offset;

        // Kameranýn karakteri takip etmesini saðla
        transform.LookAt(player);

        // Kamerayý döndürme (top-down görünümde hafif bir yatay hareket eklemek için)
        if (Input.GetKey(KeyCode.Q)) // Q tuþuna basarak kamerayý sola çevir
        {
            transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) // E tuþuna basarak kamerayý saða çevir
        {
            transform.RotateAround(player.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}
