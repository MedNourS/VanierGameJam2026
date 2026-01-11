using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private LanternLightingScript lanternLightingScript;
    [SerializeField] private Sprite front_stand;
    [SerializeField] private Sprite right_stand;
    [SerializeField] private Sprite back_stand;
    [SerializeField] private Sprite left_stand;
    [SerializeField] private Sprite front_walk;
    [SerializeField] private Sprite right_walk;
    [SerializeField] private Sprite back_walk;

    public Rigidbody2D rb;
    public Tilemap colsTilesMap;
    public Tilemap photonsTilesMap;
    public Tilemap lanternTileMap;
    public Tilemap exitTileMap;
    public TileBase unlitLanternTile;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LightRotation lightRotation;
    private PlayerLightSystem playerLightSystem;
    public LevelManager levelManager;

    public float xStep;
    public float yStep;

    public float smoothness;

    private float xPlayerControl;
    private float yPlayerControl;

    private Vector2 newPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerLightSystem = GetComponent<PlayerLightSystem>();
        newPos = new Vector2(
            rb.position.x,
            rb.position.y
        );
    }

    void Update()
    {
        xPlayerControl = 0;
        yPlayerControl = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            xPlayerControl = Input.GetKeyDown(KeyCode.A) ? -1 : (Input.GetKeyDown(KeyCode.D) ? 1 : 0);
        }
        else
        {
            yPlayerControl = Input.GetKeyDown(KeyCode.W) ? 1 : (Input.GetKeyDown(KeyCode.S) ? -1 : 0);
        }

        // if button and not in movement and can move to target 
        if (getButtons() && !inMovement() && canMove(new Vector2(
                newPos.x + xPlayerControl * xStep,
                newPos.y + yPlayerControl * yStep
            )))
        {
            newPos = new Vector2(
                newPos.x + xPlayerControl * xStep,
                newPos.y + yPlayerControl * yStep
            );
            if (!playerLightSystem.playerHasWon)
            {
                Vector3Int currentPos = photonsTilesMap.WorldToCell(transform.position);
                photonsTilesMap.SetTile(currentPos, null);
                playerLightSystem.incrementPhotonsLeft();

                if (lanternTileMap.HasTile(currentPos))
                {
                    lanternTileMap.SetTile(currentPos, unlitLanternTile);
                    lanternLightingScript.updateLanterns();
                }

                PlayerEvents.Singleton.OnPhotonsChanged?.Invoke(this, new PlayerEvents.OnPhotonsChangedEventArgs { photonsLeft = playerLightSystem.photonsLeft });

                Debug.Log("Player dead? " + checkIfPlayerDies(newPos));
            }
        }

        changeSprite();
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

    bool inMovement()
    {
        return !(0.48 <= math.abs(rb.position.x % 1) && math.abs(rb.position.x % 1) <= 0.52) && !(0.48 <= math.abs(rb.position.y % 1) && math.abs(rb.position.y % 1) <= 0.52);
    }

    bool canMove(Vector2 nextPosition)
    {
        Vector3Int gridPosition = colsTilesMap.WorldToCell((Vector3)nextPosition);
        if (!colsTilesMap.HasTile(gridPosition))
        {
            if (playerLightSystem.playerHasWon && exitTileMap.HasTile(gridPosition))
            {
                levelManager.loadNextScene();
            }
            ;
            if (playerLightSystem.playerHasWon || photonsTilesMap.HasTile(gridPosition)) return true;
            return false;
        }
        else return false;
    }

    void changeSprite()
    {
        if (inMovement())
        {
            if (lightRotation.targetRotation == 0f) spriteRenderer.sprite = back_walk;
            if (lightRotation.targetRotation == -270f) spriteRenderer.sprite = left_stand;
            if (lightRotation.targetRotation == -180f) spriteRenderer.sprite = front_walk;
            if (lightRotation.targetRotation == -90f) spriteRenderer.sprite = right_walk;
        }
        else
        {
            if (lightRotation.targetRotation == 0f) spriteRenderer.sprite = back_stand;
            if (lightRotation.targetRotation == -270f) spriteRenderer.sprite = left_stand;
            if (lightRotation.targetRotation == -180f) spriteRenderer.sprite = front_stand;
            if (lightRotation.targetRotation == -90f) spriteRenderer.sprite = right_stand;
        }
    }
}
