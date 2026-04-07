using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public UIDocument uIDocument;
    public PlayerStats player;

    private Label healthValueLabel;
    private Label shieldValueLabel;

    private void OnEnable()
    {
        if (uIDocument == null)
        {
            Debug.LogError("UIDocument not assigned to PlayerUI!");
            return;
        }

        var root = uIDocument.rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root visual element not found!");
            return;
        }

        healthValueLabel = root.Q<Label>("HealthValueLabel");
        shieldValueLabel = root.Q<Label>("ShieldValueLabel");

        if (healthValueLabel == null || shieldValueLabel == null)
        {
            Debug.LogError("One or more labels not found in UI document!");
        }
    }

    private void Update()
    {
        if (player == null || healthValueLabel == null || shieldValueLabel == null) 
            return;

        // Use public properties instead of private fields
        healthValueLabel.text = player.CurrentHealth.ToString();
        shieldValueLabel.text = player.CurrentShield.ToString();
    }

    
}