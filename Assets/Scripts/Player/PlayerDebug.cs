using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDebug : MonoBehaviour
{
    public PlayerStats player;
    private GlobalControls controls;

    private InputAction.CallbackContext callbackDamage;
    private InputAction.CallbackContext callbackHeal;
    private InputAction.CallbackContext callbackDOT;
    private InputAction.CallbackContext callbackHOT;

    private void Awake()
    {
        controls = new GlobalControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.DEBUG_DAMAGE.performed += OnDebugDamage;
        controls.Player.DEBUG_HEAL.performed += OnDebugHeal;
        controls.Player.DEBUG_DOT.performed += OnDebugDOT;
        controls.Player.DEBUG_HOT.performed += OnDebugHOT;
    }

    private void OnDisable()
    {
        controls.Player.DEBUG_DAMAGE.performed -= OnDebugDamage;
        controls.Player.DEBUG_HEAL.performed -= OnDebugHeal;
        controls.Player.DEBUG_DOT.performed -= OnDebugDOT;
        controls.Player.DEBUG_HOT.performed -= OnDebugHOT;

        controls.Disable();
    }

    private void OnDebugDamage(InputAction.CallbackContext ctx)
    {
        Debug.Log("DEBUG DAMAGE triggered");
        player.TakeDamage(10);
    }

    private void OnDebugHeal(InputAction.CallbackContext ctx)
    {
        Debug.Log("DEBUG HEAL triggered");
        player.Heal(10);
    }

    private void OnDebugDOT(InputAction.CallbackContext ctx)
    {
        Debug.Log("DEBUG DOT triggered");
        player.TakeDamageOverTime(20, 5f, 1f);
    }

    private void OnDebugHOT(InputAction.CallbackContext ctx)
    {
        Debug.Log("DEBUG HOT triggered");
        player.HealOverTime(20, 5f, 1f);
    }
}