using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float xStep;
    public float yStep;

    public float smoothness;

    private float xPlayerControl;
    private float yPlayerControl;

    private float targetXCoords;
    private float targetYCoords;

    private Vector2 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPos = new Vector2(
            rb.position.x,
            rb.position.y
        );
    }

    // Update is called once per frame
    void Update()
    {

        xPlayerControl = Input.GetKeyDown(KeyCode.A) ? -1 : (Input.GetKeyDown(KeyCode.D) ? 1 : 0);
        yPlayerControl = Input.GetKeyDown(KeyCode.W) ? 1 : (Input.GetKeyDown(KeyCode.S) ? -1 : 0);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            nextPos = new Vector2(
                rb.position.x + xPlayerControl * xStep,
                rb.position.y + yPlayerControl * yStep
            );
        }

        rb.position = Vector2.Lerp(rb.position, nextPos, Time.deltaTime * smoothness);
    }
}
