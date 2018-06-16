using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircleSelect : MonoBehaviour, IScrollHandler {

    [Tooltip("How many options the user can select.")]
    public GameObject handle;
    public float keepVisibleTime;
    public Image[] pies;

    private float t;
    private int last = -1;

    public void OnScroll(PointerEventData eventData)
    {
        if (last > 0)
            pies[last].color = Color.white;
        handle.SetActive(true);
        float a = Vector2.SignedAngle(Vector2.up, eventData.scrollDelta) + 180;
        float step = 361.0f / pies.Length;
        int id = Mathf.FloorToInt(a / step);
        t = keepVisibleTime;
        onSelect(id);
    }

    private void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0)
            handle.SetActive(false);
    }

    // This does not require a click
    public void onSelect(int id)
    {
        last = id;
        pies[id].color = Color.red;
        Debug.Log(id);
    }
}
