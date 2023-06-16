using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private bool isDragging = false;
    private Quaternion initialRotation;
    private Vector3 initialMousePosition;
    private Vector3 initialPosition;
    private RaycastHit hit; // Declare the RaycastHit variable outside the block

    void Start()
    {
        // ���������� ��������� ������� �������
        initialPosition = transform.position;
    }

    void Update()
    {
        // ���������, ������ �� ������ ������ ����
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ������� ��� �� ���� � ���������, ����� �� �� � ������
            if (Physics.Raycast(ray, out hit))
            {
                // ���������, ������ �� �� � ������ � ����� "drag"
                if (hit.collider.CompareTag("drag"))
                {
                    isDragging = true;

                    // ���������� ��������� ���������� �������
                    initialRotation = hit.collider.gameObject.transform.rotation;

                    // ���������� ��������� ������� ����
                    initialMousePosition = Input.mousePosition;
                }
            }
        }

        // ���������, �������� �� ������ ������ ����
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // ���� ������ ������ ���� ������ � ������ ���������
        if (isDragging && Input.GetMouseButton(1))
        {
            // ��������� �������� ���� ������������ ��������� �������
            Vector3 mouseOffset = Input.mousePosition - initialMousePosition;

            // ������������ ������ �� ������ �������� ����
            Quaternion rotation = Quaternion.Euler(-mouseOffset.y, mouseOffset.x, 0f);
            hit.collider.gameObject.transform.rotation = initialRotation * rotation;
        }

        // ���� ������ �������� � ���� ��������� �������, ���������� ��� ��������
        if (!isDragging && transform.position == initialPosition)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
