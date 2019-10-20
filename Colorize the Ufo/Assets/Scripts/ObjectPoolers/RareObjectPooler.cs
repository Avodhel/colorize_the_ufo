using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareObjectPooler : ObjectPooler {

    public static RareObjectPooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }
}
