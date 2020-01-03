using UnityEngine;

public class Meteor : SpaceObject {

    [SerializeField]
    private Sprite[] meteorTypeSprites;

    private new void Start()
    {
        base.Start();
        ChooseMeteorType();
    }

    private new void Update()
    {
        base.Update();
    }

    private void ChooseMeteorType()
    {

        spriteRenderer.sprite = meteorTypeSprites[Random.Range(0, meteorTypeSprites.Length)];
    }
}
