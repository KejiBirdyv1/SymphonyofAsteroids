using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    public Transform target;
    private Quaternion targetRotation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 45f);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -225f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -45f);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -90f);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 225f);
        }

        // Apply the target rotation
        target.rotation = targetRotation;
    }
}
