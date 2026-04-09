using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 100f;
    float xRotation = 0f;

    private Transform player;

    private GameManager gameManager;

    void Awake()
    {
        player = transform.root; // Player
        gameManager = Facade.Instance.GameManager;
    }

    void Update()
    {
        if(!gameManager.CanPlay)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 좌우 회전 → Player
        player.Rotate(Vector3.up * mouseX);

        // 상하 회전 → CameraRoot
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
