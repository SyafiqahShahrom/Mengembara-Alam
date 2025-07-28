using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    [HideInInspector]
    public bool isDroppedInValidZone = false; // Flag to control snapping

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        isDroppedInValidZone = false; // Reset flag on new drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        if (!isDroppedInValidZone)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}