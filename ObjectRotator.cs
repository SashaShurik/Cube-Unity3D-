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
        // Запоминаем начальную позицию объекта
        initialPosition = transform.position;
    }

    void Update()
    {
        // Проверяем, нажата ли правая кнопка мыши
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Пускаем луч от мыши и проверяем, попал ли он в объект
            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, попали ли мы в объект с тегом "drag"
                if (hit.collider.CompareTag("drag"))
                {
                    isDragging = true;

                    // Запоминаем начальную ориентацию объекта
                    initialRotation = hit.collider.gameObject.transform.rotation;

                    // Запоминаем начальную позицию мыши
                    initialMousePosition = Input.mousePosition;
                }
            }
        }

        // Проверяем, отпущена ли правая кнопка мыши
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        // Если правая кнопка мыши зажата и объект вращается
        if (isDragging && Input.GetMouseButton(1))
        {
            // Вычисляем смещение мыши относительно начальной позиции
            Vector3 mouseOffset = Input.mousePosition - initialMousePosition;

            // Поворачиваем объект на основе смещения мыши
            Quaternion rotation = Quaternion.Euler(-mouseOffset.y, mouseOffset.x, 0f);
            hit.collider.gameObject.transform.rotation = initialRotation * rotation;
        }

        // Если объект вернулся в свою начальную позицию, сбрасываем его вращение
        if (!isDragging && transform.position == initialPosition)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
