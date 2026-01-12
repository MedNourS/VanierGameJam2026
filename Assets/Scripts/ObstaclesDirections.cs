using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "ObstaclesDirections", menuName = "Scriptable Objects/ObstaclesDirections")]
public class ObstaclesDirections : ScriptableObject
{
    public TileBase bottomObstacle;
    public TileBase upObstacle;
   public TileBase leftObstacle;
    public TileBase rightObstacle;
}
