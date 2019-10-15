using UnityEngine;

public class BordersControl : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log(col.transform.tag);

        //if (transform.gameObject.tag == "yokEtSinir"  &&
        //    col.transform.tag        != "engellerTag" &&
        //    col.transform.tag        != "griEngelTag" &&
        //    col.transform.tag        != "randomEngelTag")
        //{
        Destroy(col.gameObject);
        //}
    }
}
