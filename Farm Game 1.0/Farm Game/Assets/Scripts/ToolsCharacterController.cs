using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCharacterController : MonoBehaviour
{
    private CharacterController2D character;
    private Rigidbody2D rgbd2d;
    [SerializeField] private float offsetDistance = 1f;
    [SerializeField] private float sizeOfInteractableArea = 1.2f;
    // Toolbar_UI
    public Player player;
    public string tool;
    public ItemData item;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //UseToolWorld();
        }
    }
    

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.rigidBody2D.position * offsetDistance;

        string toolName = player.inventory.toolbar.slots[GameManager.instance.selectedItemIndex].itemName;
        if (toolName == null || toolName == "") { return false;}
        if (item.onAction == null) { return false;}

        bool complete = item.onAction.OnApply(position);
        
        return complete;
    }

}