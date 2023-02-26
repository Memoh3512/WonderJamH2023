using UnityEngine;
using UnityEngine.EventSystems;

public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum ButtonType
    {
        Quit,
        Start,
        ReStart,
    }
    public GameObject showOnHover;
    public ButtonType button_type;
    public Animator DoorOpen;
    public GameObject allMenu;
    
    private void Start()
    {
        if (DoorOpen)
        {
            DoorOpen.enabled = false;
        }
        if (showOnHover)
        {
            showOnHover.SetActive(false);
        }
        if (Camera.main.GetComponent<Animator>())
        {
            Camera.main.GetComponent<Animator>().enabled = false;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        if (showOnHover)
        {
            showOnHover.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        if (showOnHover)
        {
            showOnHover.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (button_type)
        {
            case ButtonType.Quit: Application.Quit();break;
            case ButtonType.Start: 
                Camera.main.GetComponent<Animator>().enabled = true;
                if (DoorOpen)
                {
                    DoorOpen.enabled = enabled;
                    
                    SoundPlayer.instance.PlaySFX("sfx/Porte de casino ouvre");
                }
                SoundPlayer.instance.SetMusic(Songs.gameplay, 2f, TransitionBehavior.Stop);
                Destroy(allMenu);
                break;
            case ButtonType.ReStart:
                SceneChanger.ChangeScene(SceneTypes.MainMenu);
                break;
                
        }
        // Handle the button click event here
    }
}
