using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneTypes
{
    
    MainMenu = 0,
    GameplayScene = 1,
    WinScene = 2,
    LoseScene = 3,
    
}

public enum TransitionTypes
{
    
    CrossFade,
    
}

public class SceneChanger : MonoBehaviour
{

    public SceneTypes nextScene = 0;
    public TransitionTypes transition;

    public void ChangeScene()
    {

        LevelLoader.instance.LoadScene(nextScene, transition);

    }
    
    public static void ChangeScene(SceneTypes scene, TransitionTypes transitionType = TransitionTypes.CrossFade)
    {

        
        LevelLoader.instance.LoadScene(scene, transitionType);

    }

}
