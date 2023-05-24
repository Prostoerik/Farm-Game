using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropTile
{
    public int growTimer;
    public int growStage;
    public SpriteRenderer renderer;
    public Crop crop;

    public bool Complete
    {
        get {
            if (crop == null)
            {
                return false;
            }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growStage = 0;
        growTimer = 0;
        crop = null;
        renderer.gameObject.SetActive(false);
    }
}

public class CropsManager : MonoBehaviour
{
    [SerializeField] private Tile plowed;
    [SerializeField] private Tile planted;
    [SerializeField] private Tilemap targetTilemap;
    [SerializeField] private GameObject cropsSpritePrefab;
    [SerializeField] private Crop toSeed;
    [SerializeField] private List<Item> itemsToSeed;

    private float tickInterval = 1f; // Интервал между тиками в секундах
    private float timeSinceLastTick = 0f; // Время, прошедшее с последнего тика

    public List<string> itemsToSeedNames = new List<string>();

    Dictionary<Vector2Int, CropTile> crops = new Dictionary<Vector2Int, CropTile>();

    private void Start()
    {
        foreach (Item item in itemsToSeed)
        {
            itemsToSeedNames.Add(item.data.itemName);
        }
    }

    private void Update()
    {
        timeSinceLastTick += Time.deltaTime;

        if (timeSinceLastTick >= tickInterval)
        {
            Tick();
            timeSinceLastTick -= tickInterval;
        }
    }

    public void Tick()
    {
        foreach(CropTile cropTile in crops.Values)
        {
            if(cropTile.crop == null) { continue; }


            if (cropTile.Complete)
            {
                continue;
            }

            cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growthStageTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];
                cropTile.growStage += 1;
            }
        }
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void SetPlanted(Vector3Int position, string seedName)
    {
        if (Check(position))
        {
            CropTile cropTile = crops[(Vector2Int)position];
            if (cropTile.Complete)
            {
                Item droppedItem = Instantiate(cropTile.crop.yield, position, Quaternion.identity);

                crops.Remove((Vector2Int)position);
                cropTile.Harvested();
                Destroy(cropTile.renderer.gameObject);
                targetTilemap.SetTile(position, plowed); 
            }
        }
        else
        {
            targetTilemap.SetTile(position, planted);

            CropTile crop = new CropTile();
            crops.Add((Vector2Int)position, crop);
            crops[(Vector2Int)position].crop = itemsToSeed[itemsToSeedNames.IndexOf(seedName)].data.crop;
            
            GameObject go = Instantiate(cropsSpritePrefab);
            go.transform.position = targetTilemap.CellToWorld(position) + targetTilemap.cellSize / 2f; ;
            go.SetActive(false);
            crop.renderer = go.GetComponent<SpriteRenderer>();
        }
    }
}
