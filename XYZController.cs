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
            // Проверяем, было ли нажатие на контроллер
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
            // Получаем ввод от мыши для поворота
            float rotateX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotateY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Вращаем контроллер по своим осям XYZ на месте
            transform.Rotate(Vector3.up, rotateX, Space.Self);
            transform.Rotate(Vector3.left, rotateY, Space.Self);

            // Вращаем камеру вокруг контроллера
            cameraTransform.RotateAround(transform.position, Vector3.up, rotateX);
            cameraTransform.RotateAround(transform.position, Vector3.left, rotateY);
        }
    }
}
