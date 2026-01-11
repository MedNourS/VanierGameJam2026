using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLightSystem : MonoBehaviour
{
    [SerializeField] private Tilemap photons;
    [SerializeField] private Tilemap obstacles;
    [SerializeField] private TileBase photonTile;
    [SerializeField] private LightRotation lightRotation;
    private TilemapCollider2D photonsCollider;
    public int photonsLeft;
    private Vector3Int nextTile;
    void Start()
    {
        photonsCollider = photons.GetComponent<TilemapCollider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 1; i < photonsLeft + 1; i++)
            {
                Vector3Int pos = photons.WorldToCell(transform.position);
                nextTile = pos + lightRotation.lightDirection * i;

                if(!obstacles.HasTile(nextTile))
                {
                    photons.SetTile(nextTile, photonTile);
                    photonsLeft--;
                }
                else if (photons.HasTile(nextTile))
                {
                    i--;
                    continue;
                }
                else i = photonsLeft;
            }
        }
    }


}
