using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float moveSpeed = 5f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 10f;

    private void Update()
    {
        // ��������� �����������/��������� ������
        HandleZoom();

        // ��������� �������� ������
        HandleMovement();
    }

    private void HandleZoom()
    {
        // �������� ���� �� �������� ����
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        // �������� ������� ������ ��� �����������/���������
        Vector3 cameraPosition = transform.position;
        cameraPosition += transform.forward * zoomInput * zoomSpeed;

        // ������������ ��������� �����������/��������� ������
        cameraPosition = ClampCameraPosition(cameraPosition);

        // ��������� ����� ������� ������
        transform.position = cameraPosition;
    }

    private void HandleMovement()
    {
        // �������� ���� �� ������ WS
        float moveInput = Input.GetAxis("Vertical");

        // �������� ������� ������ ��� �������� ������/�����
        Vector3 cameraPosition = transform.position;
        cameraPosition += transform.forward * moveInput * moveSpeed * Time.deltaTime;

        // ��������� ����� ������� ������
        transform.position = cameraPosition;
    }

    private Vector3 ClampCameraPosition(Vector3 position)
    {
        // ������������ ��������� �����������/��������� ������ � �������� minZoomDistance � maxZoomDistance
        float distance = Vector3.Distance(position, transform.parent.position);
        float clampedDistance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

        // ��������� ����� ������� ������
        Vector3 direction = (position - transform.parent.position).normalized;
        Vector3 clampedPosition = transform.parent.position + direction * clampedDistance;

        return clampedPosition;
    }
}
