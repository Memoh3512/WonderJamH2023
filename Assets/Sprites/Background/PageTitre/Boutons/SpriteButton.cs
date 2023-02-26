using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ButtonType
    {
        Quit,
        Start
    }
    public GameObject showOnHover;
    public ButtonType button_type;
    public Animator DoorOpen;
    
    private void Start()
    {
        DoorOpen.enabled = false;
        showOnHover.SetActive(false);
        Camera.main.GetComponent<Animator>().enabled = false;
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
        switch (button_type)
        {
            case ButtonType.Quit: Application.Quit();break;
            case ButtonType.Start: 
                Camera.main.GetComponent<Animator>().enabled = true;
                DoorOpen.enabled = enabled;
                break;
        }
        // Handle the button click event here
    }
    public void StartGame()
    {
        GetComponent<SceneChanger>().ChangeScene();
    }
}
