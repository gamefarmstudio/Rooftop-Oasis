using UnityEngine;

public class CanItouchThisThing : MonoBehaviour
{
    public GameObject redUI; // Your 3D Cube

    private void Start()
    {
        if (redUI == null)
        {
            // IMPORTANT: The name inside the quotes must match the object's name in your Hierarchy EXACTLY.
            redUI = GameObject.Find("RedUI");
        }
        // It's still good practice to hide the cube right when the game starts
        if (redUI != null)
        {
            redUI.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Decorations") || collision.gameObject.CompareTag("object"))
        {
            redUI.SetActive(true);

            // THE FIX: Add an offset so it hovers 2 units above the object's center
            Vector3 hoverOffset = new Vector3(0, 2f, 0);
            redUI.transform.position = collision.transform.position + hoverOffset;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Decorations") || collision.gameObject.CompareTag("object"))
        {
            redUI.SetActive(false);
        }
    }
}