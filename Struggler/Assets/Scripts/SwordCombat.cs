using UnityEngine;
using System.Collections;

public class SwordCombat : MonoBehaviour
{
    public Transform sword;
    public Transform player;
    private Quaternion originalRotation;
    private float cooldown = 0.5f; // Cooldown duration
    private float lastClickTime = -0.5f; // Initialize to allow immediate first click
    void Start()
    {
        // Store the initial rotation
        originalRotation = sword.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastClickTime >= cooldown)
        {
            RotateSword();
            lastClickTime = Time.time; // Update the last click time
        }
    }

    private void RotateSword()
    {
        // Define the target rotation (original rotation - 60 degrees on the Z axis)
        Quaternion targetRotation = Quaternion.Euler(sword.rotation.eulerAngles.x,
                sword.rotation.eulerAngles.y,
                sword.rotation.eulerAngles.z );
        if (player.localScale.x > 0)
        {
                targetRotation = Quaternion.Euler(
                sword.rotation.eulerAngles.x,
                sword.rotation.eulerAngles.y,
                sword.rotation.eulerAngles.z - 60
            );
        }
        else if (player.localScale.x < 0) {
               targetRotation = Quaternion.Euler(
               sword.rotation.eulerAngles.x,
               sword.rotation.eulerAngles.y,
               sword.rotation.eulerAngles.z + 60
           );
        }
        

        // Start a coroutine to smoothly rotate to the target and back to the original
        StartCoroutine(SmoothRotate(targetRotation, 0.1f)); // 0.2f seconds to reach target rotation
    }

    IEnumerator SmoothRotate(Quaternion targetRotation, float duration)
    {
        float time = 0f;
        Quaternion startRotation = sword.rotation;

        // Smoothly rotate to the target rotation
        while (time < duration)
        {
            time += Time.deltaTime;
            sword.rotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);
            yield return null;
        }

        // Ensure the rotation completes precisely
        sword.rotation = targetRotation;

        // Wait briefly before returning to the original position
        yield return new WaitForSeconds(0.2f);

        // Smoothly rotate back to the original rotation
        time = 0f;
        startRotation = sword.rotation;
        while (time < duration)
        {
            time += Time.deltaTime;
            sword.rotation = Quaternion.Slerp(startRotation, originalRotation, time / duration);
            yield return null;
        }

        sword.rotation = originalRotation;  // Ensure exact alignment with original
    }
}
