using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevel("UI_scene");
        //Application.UnloadLevel("Startmenu_scene");    
    }
}
