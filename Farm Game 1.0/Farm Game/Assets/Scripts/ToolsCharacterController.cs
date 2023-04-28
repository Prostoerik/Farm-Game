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
    private Toolbar_UI toolbarController;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        toolbarController = GetComponent<Toolbar_UI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //UseToolWorld();
        }
    }
    
    /*
    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position + character.rigidBody2D.position * offsetDistance;

        Item item = toolbarController.GetItem;
        if (item == null) { return false;}
        if (item.onAction == null) { return false;}

        bool complete = item.onAction.OnApply(position);
        
        return complete;
    }
    */
}
