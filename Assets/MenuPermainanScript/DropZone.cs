using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string acceptedItemTag; // e.g., "Glass", "Tissue", etc.

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (droppedItem != null && eventData.pointerDrag.CompareTag(acceptedItemTag))
        {
            droppedItem.isDroppedInValidZone = true;
            droppedItem.transform.SetParent(transform);
            droppedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Optionally inform GameManager
            FindObjectOfType<GameManager>().CorrectItemSorted();
        }
    }
}
