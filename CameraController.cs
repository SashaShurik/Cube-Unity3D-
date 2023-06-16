using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float moveSpeed = 5f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 10f;

    private void Update()
    {
        // Обработка приближения/отдаления камеры
        HandleZoom();

        // Обработка движения камеры
        HandleMovement();
    }

    private void HandleZoom()
    {
        // Получаем ввод от колесика мыши
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        // Изменяем позицию камеры для приближения/отдаления
        Vector3 cameraPosition = transform.position;
        cameraPosition += transform.forward * zoomInput * zoomSpeed;

        // Ограничиваем дистанцию приближения/отдаления камеры
        cameraPosition = ClampCameraPosition(cameraPosition);

        // Применяем новую позицию камеры
        transform.position = cameraPosition;
    }

    private void HandleMovement()
    {
        // Получаем ввод от клавиш WS
        float moveInput = Input.GetAxis("Vertical");

        // Изменяем позицию камеры для движения вперед/назад
        Vector3 cameraPosition = transform.position;
        cameraPosition += transform.forward * moveInput * moveSpeed * Time.deltaTime;

        // Применяем новую позицию камеры
        transform.position = cameraPosition;
    }

    private Vector3 ClampCameraPosition(Vector3 position)
    {
        // Ограничиваем дистанцию приближения/отдаления камеры в пределах minZoomDistance и maxZoomDistance
        float distance = Vector3.Distance(position, transform.parent.position);
        float clampedDistance = Mathf.Clamp(distance, minZoomDistance, maxZoomDistance);

        // Вычисляем новую позицию камеры
        Vector3 direction = (position - transform.parent.position).normalized;
        Vector3 clampedPosition = transform.parent.position + direction * clampedDistance;

        return clampedPosition;
    }
}
