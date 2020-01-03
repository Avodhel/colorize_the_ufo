using UnityEngine;

public class Obstacle : MonoBehaviour, IObstacle
{
    private Rigidbody2D rb2d;

    private Color[] colors;

    public void OnBecameInvisible() //screen exited
    {
        gameObject.SetActive(false);
    }

    public void ObstacleMovement()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -2); // engellerimizin hareket etmesi için.
    }

    public void ChangeColor()
    {
        colors = GameControl.gameManager.colors;
        for (int a = 0; a < transform.childCount; a++)
        {
            if (transform.GetChild(a).tag == "randomEngelTag")
            {
                transform.GetChild(a).GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)]; //renk degistir
            }
        }
    }
}
