using UnityEngine;

public class RotateAroundCamera : MonoBehaviour
{
    public Transform targetObject;
    public float rotationSpeed = 50f;
    public float distanceFromTarget = 5f;

    void Update()
    {
        // Вращаем камеру вокруг объекта
        transform.RotateAround(targetObject.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // Вычисляем новую позицию камеры
        Vector3 desiredPosition = targetObject.position - transform.forward * distanceFromTarget;

        // Применяем новую позицию камеры
        transform.position = desiredPosition;

        // Направляем камеру на объект
        transform.LookAt(targetObject);
    }
}
