using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
	private float restartDelay = 5f;                // Time to wait before restarting the level
    private float timer; 
	private Animator anim;                          // Reference to the animator component.
	private float restartTimer;                     // Timer to count up to restarting the level
    private Player player;
    private Text text;
    private int team1Army;
    private int team2Army;


    void Awake ()
	{
		// Set up the reference.
		anim = GetComponent <Animator> ();
        text = GetComponent<Text>();
        player = new Player();
	}
	
	
	void Update ()
	{
        timer += Time.deltaTime;
        
        // If the player has run out of health...
		if(timer >= 4)
		{
            GameObject.Find("HUDCanvas/EndGameMessage").GetComponent<Text>().text = string.Format(" {0} wins",player.CurrentTeam);

            // ... tell the animator the game is over.
            anim.SetTrigger ("GameOver");
            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;
			
			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				Application.LoadLevel("ProbableMapScene");
			}
		}
	}

   
}