using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject showOnHover;


    private void Start()
    {
        showOnHover.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        showOnHover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        showOnHover.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle the button click event here
    }
}
