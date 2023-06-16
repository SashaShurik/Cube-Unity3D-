using UnityEngine;

public class RotateAroundCamera : MonoBehaviour
{
    public Transform targetObject;
    public float rotationSpeed = 50f;
    public float distanceFromTarget = 5f;

    void Update()
    {
        // ������� ������ ������ �������
        transform.RotateAround(targetObject.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // ��������� ����� ������� ������
        Vector3 desiredPosition = targetObject.position - transform.forward * distanceFromTarget;

        // ��������� ����� ������� ������
        transform.position = desiredPosition;

        // ���������� ������ �� ������
        transform.LookAt(targetObject);
    }
}
