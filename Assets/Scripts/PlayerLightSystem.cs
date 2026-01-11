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
        Vector3Int pos = photons.WorldToCell(transform.position);
        photons.SetTile(pos, photonTile);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int iterations = photonsLeft;
            for (int i = 0; i < iterations; i++)
            {
                Vector3Int pos = photons.WorldToCell(transform.position);
                nextTile = pos + lightRotation.lightDirection * i;


                if (photons.HasTile(nextTile))
                {
                    iterations++;
                }
                else if (obstacles.HasTile(nextTile))
                {
                    i = iterations;
                }
                else
                {
                    photons.SetTile(nextTile, photonTile);
                    photonsLeft--;
                }
            }
        }
    }

    public void incrementPhotonsLeft()
    {
        photonsLeft++;
    }
}
