using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 8f;
    public float height = 5f;
    public float xSpeed = 120f;      // Horizontal rotation speed
    public float ySpeed = 80f;       // Vertical rotation speed
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float currentX = 0f;
    private float currentY = 20f;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
        // Initial angles from current rotation
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Get mouse movement (always active, no button needed)
        float mouseX = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        currentX += mouseX;
        currentY -= mouseY;
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        // Calculate position
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, 0, -distance);
        // Add height
        desiredPosition.y += height;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}
