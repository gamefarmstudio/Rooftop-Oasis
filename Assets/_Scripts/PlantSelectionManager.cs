using UnityEngine;
using UnityEngine.UI;

public class PlantSelectionManager : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;

    // This makes it easy for other scripts to see which is selected
    public int GetSelectedPlantID()
    {
        if (toggle1.isOn) return 1;
        if (toggle2.isOn) return 2;
        return 0; // None selected
    }
}