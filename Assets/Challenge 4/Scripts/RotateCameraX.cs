using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private Transform player;

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");

        transform.Rotate(Vector3.up, mouseX * mouseSensitivity * Time.deltaTime);

        transform.position = player.position;
    }
}