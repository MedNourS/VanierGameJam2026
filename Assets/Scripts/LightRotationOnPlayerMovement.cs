using UnityEngine;

public class LightRotationOnPlayerMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private float targetRotation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            targetRotation = 0;
        if (Input.GetKeyDown(KeyCode.D))
            targetRotation = -90;
        if (Input.GetKeyDown(KeyCode.S))
            targetRotation = -180;
        if (Input.GetKeyDown(KeyCode.A))
            targetRotation = -270;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, targetRotation)), rotationSpeed * Time.deltaTime);
    }
}
