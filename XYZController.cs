using UnityEngine;

public class XYZController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Transform cameraTransform;

    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���������, ���� �� ������� �� ����������
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isRotating = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // �������� ���� �� ���� ��� ��������
            float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // ������� ���������� �� ����� ���� XYZ �� �����
            transform.Rotate(Vector3.up, rotateX, Space.Self);
            transform.Rotate(Vector3.left, rotateY, Space.Self);

            // ������� ������ ������ �����������
            cameraTransform.RotateAround(transform.position, Vector3.up, rotateX);
            cameraTransform.RotateAround(transform.position, Vector3.left, rotateY);
        }
    }
}
