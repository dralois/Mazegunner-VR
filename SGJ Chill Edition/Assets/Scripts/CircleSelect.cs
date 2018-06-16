using UnityEngine;
using UnityEngine.EventSystems;

public class CircleSelect : MonoBehaviour, IScrollHandler {

    [Tooltip("How many options the user can select.")]
    public int options;

    public void OnScroll(PointerEventData eventData)
    {
        float a = Vector2.SignedAngle(Vector2.up, eventData.scrollDelta) + 180;
        float step = 361.0f / options;
        int id = Mathf.FloorToInt(a / step);
        onSelect(id);
    }

    // This does not require a click
    public void onSelect(int id)
    {

    }
}
