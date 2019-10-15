using UnityEngine;

public class Obstacle : MonoBehaviour {

    //private Color[] colors;

    //private void Awake()
    //{
    //    colors = GameControl.gameManager.colors;
    //}

    //private void obstacleManager()
    //{
    //    if (transform.position.y <= -6f) // engel sahneden çıktığında rengini ve triggerını değiştirmek için koşul
    //    {
    //        for (int a = 0; a < transform.childCount; a++)
    //        {
    //            if (transform.GetChild(a).tag == "randomEngelTag")
    //            {
    //                transform.GetChild(a).GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)]; //renk degistir

    //                if (transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger == true) //istriggeri açıksa 
    //                {
    //                    transform.GetChild(a).GetComponent<BoxCollider2D>().isTrigger = false; //kapat
    //                }
    //            }
    //        }
    //    }
    //}
}
