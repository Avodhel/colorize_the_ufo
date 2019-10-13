using UnityEngine;

public class Meteor : SpaceObject {

    [SerializeField]
    private Sprite[] meteorTypeSprites;

    private new void Start()
    {
        base.Start();
        chooseMeteorType();
    }

    private new void Update()
    {
        base.Update();
    }

    private void chooseMeteorType()
    {

        spriteRenderer.sprite = meteorTypeSprites[Random.Range(0, meteorTypeSprites.Length)];
    }
}
