﻿using UnityEngine;

public class SinirlarKontrol : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D col)
    {
        if (transform.gameObject.tag == "yokEtSinir"  &&
            col.transform.tag        != "engellerTag" &&
            col.transform.tag        != "griEngelTag" &&
            col.transform.tag        != "randomEngelTag")
        {
            Destroy(col.gameObject);
        }
    }
}