using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMesh;
    public Rigidbody2D rb;
    public Tilemap colsTilesMap;
    public Tilemap photonsTilesMap;
    public Tilemap lanternTileMap;
    public TileBase unlitLanternTile;

    public PlayerLightSystem playerLightSystem;

    public float xStep;
    public float yStep;

    public float smoothness;

    public int lanternCount;

    private float xPlayerControl;
    private float yPlayerControl;

    private Vector2 newPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lanternCount = 0;

        TileBase[] lanternTiles = lanternTileMap.GetTilesBlock(lanternTileMap.cellBounds);

        for (int i = 0; i < lanternTiles.Length; i++)
        {
            if (lanternTiles[i] != null)
            {
                lanternCount++;
            }
        }

        playerLightSystem = GetComponent<PlayerLightSystem>();
        newPos = new Vector2(
            rb.position.x,
            rb.position.y
        );
    }

    void Update()
    {
        xPlayerControl = Input.GetKeyDown(KeyCode.A) ? -1 : (Input.GetKeyDown(KeyCode.D) ? 1 : 0);
        yPlayerControl = Input.GetKeyDown(KeyCode.W) ? 1 : (Input.GetKeyDown(KeyCode.S) ? -1 : 0);

        // if button and not in movement and can move to target 
        if (getButtons() && canMove(new Vector2(
                newPos.x + xPlayerControl * xStep,
                newPos.y + yPlayerControl * yStep
            )))
        {
            Vector3Int currentPos = photonsTilesMap.WorldToCell(transform.position);
            photonsTilesMap.SetTile(currentPos, null);
            playerLightSystem.incrementPhotonsLeft();

            if (lanternTileMap.HasTile(currentPos))
            {
                lanternTileMap.SetTile(currentPos, unlitLanternTile);
            }

            newPos = new Vector2(
                newPos.x + xPlayerControl * xStep,
                newPos.y + yPlayerControl * yStep
            );
            textMesh.text = "<" + playerLightSystem.photonsLeft + " Photons Left>";

            Debug.Log(checkIfPlayerDies(newPos));
        }
    }
    private bool checkIfPlayerDies(Vector2 playerPos)
    {
        Vector3Int currentPos = photonsTilesMap.WorldToCell(playerPos);
        if (photonsTilesMap.HasTile(currentPos + Vector3Int.up) || photonsTilesMap.HasTile(currentPos + Vector3Int.down) || photonsTilesMap.HasTile(currentPos + Vector3Int.left) || photonsTilesMap.HasTile(currentPos + Vector3Int.right) ||
        photonsTilesMap.HasTile(currentPos + Vector3Int.up + Vector3Int.right) || photonsTilesMap.HasTile(currentPos + Vector3Int.down + Vector3Int.right) || photonsTilesMap.HasTile(currentPos + Vector3Int.down + Vector3Int.left) || photonsTilesMap.HasTile(currentPos + Vector3Int.up + Vector3Int.left))
        {
            return false;
        }
        else return true;
    }

    void FixedUpdate()
    {
        rb.MovePosition(Vector2.Lerp(rb.position, newPos, Time.deltaTime * smoothness));
    }

    private bool getButtons()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
    }

    private bool canMove(Vector2 nextPosition)
    {
        Vector3Int gridPosition = colsTilesMap.WorldToCell((Vector3)nextPosition);
        return (!colsTilesMap.HasTile(gridPosition) && photonsTilesMap.HasTile(gridPosition));
    }
}
