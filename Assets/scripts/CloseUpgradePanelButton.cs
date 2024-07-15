using UnityEngine;
using UnityEngine.UI;

public class CloseUpgradePanelButton : MonoBehaviour
{
    public GameObject upgradePanelParent;  // Reference to the parent GameObject of the upgrade panel

    private void Start()
    {
        // Ensure upgrade panel parent is assigned properly in the Inspector
        if (upgradePanelParent == null)
        {
            Debug.LogError("Upgrade Panel Parent is not assigned to the CloseUpgradePanelButton script!");
        }
    }

    public void OnButtonClick()
    {
        // Deactivate the upgrade panel parent
        if (upgradePanelParent != null)
        {
            upgradePanelParent.SetActive(false);
        }

        // Unpause the game
        Time.timeScale = 1f;  // Resume game time
    }
}
