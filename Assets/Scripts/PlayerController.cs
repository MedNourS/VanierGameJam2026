using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public Tilemap colsTilesMap;
    public Tilemap groundTilesMap;

    public float xStep;
    public float yStep;

    public float smoothness;

    private float xPlayerControl;
    private float yPlayerControl;

    private Vector2 newPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            newPos = new Vector2(
                newPos.x + xPlayerControl * xStep,
                newPos.y + yPlayerControl * yStep
            );
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(Vector2.Lerp(rb.position, newPos, Time.deltaTime * smoothness));
    }

    private bool getButtons()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D);
    }

    private bool notMoving()
    {
        return (0.49f <= math.abs(rb.position.x % 1) && math.abs(rb.position.x % 1) <= 0.5f) && (0.49f <= math.abs(rb.position.y % 1) && math.abs(rb.position.y % 1) <= 0.5f);
    }

    private bool canMove(Vector2 nextPosition)
    {
        Vector3Int gridPosition = colsTilesMap.WorldToCell((Vector3)nextPosition);
        return (!colsTilesMap.HasTile(gridPosition) && groundTilesMap.HasTile(gridPosition));
    }
}
