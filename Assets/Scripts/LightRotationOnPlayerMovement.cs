using UnityEngine;

public class LightRotationOnPlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float targetRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            targetRotation = 0;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            targetRotation = -90;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            targetRotation = -180;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            targetRotation = -270;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, targetRotation)), rotationSpeed * Time.deltaTime);
    }
}
