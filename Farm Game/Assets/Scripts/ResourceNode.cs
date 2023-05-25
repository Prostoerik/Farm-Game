using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ResourceNode : ToolHit
{
    [SerializeField] private GameObject collectable;
    [SerializeField] private int dropCount = 5;
    [SerializeField] private float spread = 0.7f;
    [SerializeField] private ResourceNodeType nodeType;
    //[SerializeField] private Item item;
    [SerializeField] private int itemCountInOneDrop = 1;
    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(collectable);
            go.transform.position = position;
        }
        
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}
