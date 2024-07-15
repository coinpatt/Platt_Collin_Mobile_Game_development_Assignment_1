using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for accessing Unity's UI elements

public class AutoClickTMPROButton : MonoBehaviour
{
    public Button button; // Reference to the Unity Button component
    public float clickInterval = 3f; // Interval between clicks in seconds

    private void Start()
    {
        // Check if button reference is set
        if (button == null)
        {
            Debug.LogError("Button reference is not set. Please assign it in the Inspector.");
            return;
        }

        // Start the coroutine to click the button periodically
        StartCoroutine(ClickButtonPeriodically());
    }

    private IEnumerator ClickButtonPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(clickInterval);

            // Simulate a click on the button
            button.onClick.Invoke();

            // Optionally, you can also log a message each time the button is clicked
            Debug.Log("Button clicked.");

            // You can add additional logic here if needed

        }
    }
}
