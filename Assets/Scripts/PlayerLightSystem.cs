using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLightSystem : MonoBehaviour
{
    [SerializeField] private Tilemap plates;
    [SerializeField] private Tilemap obstacles;
    [SerializeField] private TileBase lightTile;
    [SerializeField] private LightRotationOnPlayerMovement lightRotationOnPlayerMovement;
    private TilemapCollider2D platesCollider;
    public int photonsLeft;
    private Vector3Int nextTile;
    void Start()
    {
        platesCollider = plates.GetComponent<TilemapCollider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 1; i < photonsLeft + 1; i++)
            {
                Vector3Int pos = plates.WorldToCell(transform.position);
                nextTile = pos + lightRotationOnPlayerMovement.lightDirection * i;

                if(!obstacles.HasTile(nextTile))
                {
                    plates.SetTile(nextTile, lightTile);
                    photonsLeft--;
                }
                else if (plates.HasTile(nextTile)) continue;
                else i = photonsLeft;
            }
        }
    }


}
