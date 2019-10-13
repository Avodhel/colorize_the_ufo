using UnityEngine;

public class GameSpeedControl : MonoBehaviour {

    float gameSpeedChangeTime = 0f;
    bool gameSpeedControl = true;

	private void Start ()
    {
        Time.timeScale = 1;
	}
	
	private void Update ()
    {
        gameSpeedChange();
    }

    private void gameSpeedChange()
    {
        //increase speed
        if (gameSpeedControl)
        {
            gameSpeedChangeTime += Time.deltaTime;

            while (gameSpeedChangeTime > 8f) 
            {
                Time.timeScale += 0.02f; //hizlanma birimi
                gameSpeedChangeTime = 0f;
                if (Time.timeScale > Random.Range(1.4f, 1.7f)) //hiz için üst sınır
                {
                    gameSpeedControl = false;
                }
            }
        }
        //reduce speed
        else
        {
            gameSpeedChangeTime += Time.deltaTime;

            while (gameSpeedChangeTime > 5f)
            {
                Time.timeScale -= 0.06f; //azalma birimi
                gameSpeedChangeTime = 0f;
                if (Time.timeScale < Random.Range(1f, 1.2f)) //hiz için alt sinir
                {
                    gameSpeedControl = true;
                }
            }
        }
    }
}
