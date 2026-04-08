using UnityEngine;
using UnityEngine.UI;

public class IChooseThisPlant : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;

    void Start()
    {
        // Ensure toggles are not null
        if (toggle1 == null || toggle2 == null)
        {
            Debug.LogError("Toggles are not assigned in the Inspector.");
            return;
        }
    }

    private void Awake()
    {
        toggle1.isOn = false; 
        toggle2.isOn = false;
    }

    void Update()
    {
        // Check if toggle1 is on
        if (toggle1.isOn) // <-- Changed .value to .isOn
        {
            Debug.Log("Toggle 1 is selected.");
            toggle2.isOn = false; // <-- Changed .value to .isOn
            // You can add additional logic here for when toggle1 is selected
        }

        // Check if toggle2 is on
        if (toggle2.isOn) // <-- Changed .value to .isOn
        {
            Debug.Log("Toggle 2 is selected.");
            toggle1.isOn = false; // <-- Changed .value to .isOn
            // You can add additional logic here for when toggle2 is selected
        }
    }
}
