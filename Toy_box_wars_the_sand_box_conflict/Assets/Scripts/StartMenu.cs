using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void StartGame()
    {
        Application.LoadLevel("Map_scene");
        //Application.UnloadLevel("Startmenu_scene");    
    }
}
