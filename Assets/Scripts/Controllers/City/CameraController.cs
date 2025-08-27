using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers.City
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float minX;
        [SerializeField] private float maxX;
        [SerializeField] private float smoothTime = 0.1f;

        private Vector3 targetPosition;
        private Vector3 velocity = Vector3.zero;
        private Vector2 lastInputPosition;
        private bool isDragging = false;

        public static bool isStopMoving = false;

        private void Start()
        {
            targetPosition = transform.position;
        }

        private void Update()
        {
            if (isStopMoving)
                return;

            HandleInput();

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastInputPosition = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;

                if (touch.phase == TouchPhase.Began)
                {
                    lastInputPosition = touch.position;
                    isDragging = true;
                }
                else if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    Vector2 delta = touch.position - lastInputPosition;
                    lastInputPosition = touch.position;

                    targetPosition -= Vector3.right * delta.x * moveSpeed * Time.deltaTime;
                    targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                }
            }

            if (isDragging)
            {
                Vector2 delta = (Vector2)Input.mousePosition - lastInputPosition;
                lastInputPosition = Input.mousePosition;

                targetPosition -= Vector3.right * delta.x * moveSpeed * Time.deltaTime;
                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            }
        }
    }
}