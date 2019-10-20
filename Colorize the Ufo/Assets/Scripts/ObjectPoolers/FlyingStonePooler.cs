using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingStonePooler : ObjectPooler {

    public static FlyingStonePooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }

}
