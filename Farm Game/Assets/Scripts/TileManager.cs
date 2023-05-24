using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;

    [SerializeField] private Tile interactedTile;

    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    public bool isInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if(tile != null)
        {
            if(tile.name == "Interactable" || tile.name == "Summer_Plowed" || tile.name == "Summer_Planted")
            {
                return true;
            }
        }

        return false;
    }

    public bool isPlowed(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        {
            if (tile.name == "Summer_Plowed")
            {
                return true;
            }
        }

        return false;
    }

    public bool isPlanted(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        { 
            if (tile.name == "Summer_Planted")
            {
                return true;
            }
        }

        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        if(interactableMap.GetTile(position) == interactedTile)
        {
            interactableMap.SetTile(position, hiddenInteractableTile);
        }
        else
        {
            interactableMap.SetTile(position, interactedTile);
        }
    }
}
