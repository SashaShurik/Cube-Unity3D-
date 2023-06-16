using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 10f;
    public float snapDistance = 0.1f; // Расстояние, при котором объект примагничивается к начальной позиции

    private GameObject selectedObject;
    private Vector3[] initialObjectPositions; // Массив для хранения начальных позиций объектов

    private bool gameStarted = false; // Флаг, указывающий, была ли игра запущена

    private void Awake()
    {
        // Запоминаем начальные позиции всех объектов
        GameObject[] draggableObjects = GameObject.FindGameObjectsWithTag("drag");
        initialObjectPositions = new Vector3[draggableObjects.Length];
        for (int i = 0; i < draggableObjects.Length; i++)
        {
            initialObjectPositions[i] = draggableObjects[i].transform.position;
        }
    }

    private void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            // Запуск игры - фиксируем начальные позиции объектов
            gameStarted = true;
            Debug.Log("Initial Positions:");
            for (int i = 0; i < initialObjectPositions.Length; i++)
            {
                Debug.Log("Object " + i + ": " + initialObjectPositions[i]);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = CastRay();

            if (hit.collider != null && hit.collider.CompareTag("drag"))
            {
                selectedObject = hit.collider.gameObject;
            }
        }
        else if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            selectedObject = null;
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            // Проверяем, является ли новая позиция выбранного объекта его начальной позицией
            bool isInitialPosition = false;
            for (int i = 0; i < initialObjectPositions.Length; i++)
            {
                if (Vector3.Distance(worldPosition, initialObjectPositions[i]) <= snapDistance)
                {
                    isInitialPosition = true;
                    break;
                }
            }

            // Если новая позиция является начальной позицией, то примагничиваем объект
            if (isInitialPosition)
            {
                selectedObject.transform.position = initialObjectPositions[selectedObject.GetInstanceID() % initialObjectPositions.Length];
            }
            else
            {
                selectedObject.transform.position = worldPosition;
            }
        }

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        Vector3 moveChange = Vector3.forward * scrollInput * moveSpeed * Time.deltaTime;
        if (selectedObject != null)
        {
            selectedObject.transform.Translate(moveChange);
        }
        else
        {
            float zoomInput = Input.GetAxis("Mouse ScrollWheel");
            float zoomAmount = zoomInput * zoomSpeed * Time.deltaTime;
            float newZoomDistance = Vector3.Distance(transform.position, Camera.main.transform.position) - zoomAmount;
            newZoomDistance = Mathf.Clamp(newZoomDistance, minZoomDistance, maxZoomDistance);
            Vector3 zoomDirection = (Camera.main.transform.position - transform.position).normalized;
            Camera.main.transform.position = transform.position + zoomDirection * newZoomDistance;
        }
    }

    private void OnDrawGizmos()
    {
        if (!gameStarted)
        {
            DrawInitialPositionsGizmos();
        }

        if (selectedObject != null)
        {
            DrawSelectedObjectGizmo();
        }
    }

    private void DrawInitialPositionsGizmos()
    {
        if (selectedObject != null)
        {
            Gizmos.color = Color.red;
            int objID = selectedObject.GetInstanceID();
            Vector3 initialPosition = initialObjectPositions[objID % initialObjectPositions.Length];
            Gizmos.DrawWireSphere(initialPosition, snapDistance);
            Gizmos.DrawLine(initialPosition, selectedObject.transform.position);
        }
    }

    private void DrawSelectedObjectGizmo()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(selectedObject.transform.position, 0.1f);
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
