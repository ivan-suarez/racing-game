using UnityEngine;

public class ShowSpeed : MonoBehaviour
{
    public Rigidbody carRigidbody;  // Assign your car's Rigidbody here in the Inspector

    void OnGUI()
    {
        float speed = carRigidbody.velocity.magnitude;  // Speed in units/second
        float speedKPH = speed * 3.6f; // Convert to Kilometers per hour

        GUI.Label(new Rect(10, 10, 200, 20), "Speed: " + speedKPH.ToString("F2") + " KM/H");
    }
}
