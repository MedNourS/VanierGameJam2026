using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class LanternLightingScript : MonoBehaviour
{
    [SerializeField] private Tilemap lanternsTileMap;
    [SerializeField] private Light2D lanternLightObj;
    [SerializeField] private TileBase litLantern;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var pos in lanternsTileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int tilePos = lanternsTileMap.WorldToCell(pos);
            Debug.Log(pos);
            if (lanternsTileMap.GetTile(tilePos) == litLantern)
            {
                Instantiate(lanternLightObj, tilePos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
