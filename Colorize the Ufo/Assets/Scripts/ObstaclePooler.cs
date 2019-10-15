using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : ObjectPooler {

    public static ObstaclePooler SharedInstance { get; private set; }

    public override void Awake()
    {
        SharedInstance = this;
        base.Awake();
    }
}
