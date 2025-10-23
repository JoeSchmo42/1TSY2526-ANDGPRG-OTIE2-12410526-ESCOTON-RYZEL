using UnityEngine;
using TMPro;

public class PlanetNameUI : MonoBehaviour
{
    public TextMeshProUGUI planetNameText; // Reference to UI Text
    public float detectionRange = 10f; // Adjusted for closer planets
    private GameObject[] planets;

    void Start()
    {
        // Find all objects tagged as "Planet"
        planets = GameObject.FindGameObjectsWithTag("Planet");
        planetNameText.text = ""; // Clear text initially
    }

    void Update()
    {
        planetNameText.text = ""; // Reset text
        foreach (GameObject planet in planets)
        {
            float distance = Vector3.Distance(transform.position, planet.transform.position);
            if (distance <= detectionRange)
            {
                planetNameText.text = planet.name; // Display planet name
                break;
            }
        }
    }
}