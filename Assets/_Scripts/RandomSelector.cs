using TMPro;
using UnityEngine;

public class RandomSelector : MonoBehaviour
{
    // 1. Reference your UI component in the Inspector
    public TextMeshProUGUI TextField1;
    public TextMeshProUGUI TextField2;
    // public Text myLegacyTextField; // Uncomment if using Legacy

    // 2. Your array of strings
    private string[] randomMessages = { "Oak", "Maple", "Pine", "Banyan", "Azalea", "Money Plant", "Ivy", "Snake Plant", "Spider Plant", "Peace Lily", "Monstera", "Cedar", "Fir", "Spruce", "Redwood", "Sequoia", "Willow", "Birch", "Ash", "Elm", "Walnut", "Chestnut", "Hickory", "Mahogany", "Teak", "Ebony", "Fern", "Moss", "Liverwort", "Lichen", "Duckweed", "Bamboo", "Orchid", "Tulip", "Lily", "Daisy" };

    // Function to change the text
    public void ChangeTextRandomly()
    {
        // Pick a random index from the array
        int randomIndex = Random.Range(0, randomMessages.Length);
        int randomIndex2 = Random.Range(0, randomMessages.Length);
        // Update the UI component with that string
        TextField1.text = randomMessages[randomIndex];
        TextField2.text = randomMessages[randomIndex2];
    }
}
