using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    public UIDocument uIDocument;
    public PlayerStats player;


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



    }

    private void Update()
    {

    }

    
}