using UnityEngine;

public class GameSpeedControl : MonoBehaviour {

    float gameSpeedChangeTime = 0f;
	
	private void Update ()
    {
        GameSpeedChange();
    }

    private void GameSpeedChange()
    {
        gameSpeedChangeTime += Time.deltaTime;

        while (gameSpeedChangeTime > 2f) 
        {
            GameControl.gameManager.GameSpeed("increase", 0.02f);
            gameSpeedChangeTime = 0f;
        }
    }
}
