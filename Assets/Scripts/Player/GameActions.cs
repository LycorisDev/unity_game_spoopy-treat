using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(MenuControls))]
public class GameActions : MonoBehaviour
{
    private Character _playerScript;
    private CameraManager _cameraScript;
    private MenuControls _menuControls;

    [SerializeField] private InputActionReference _horizontalMovementValue;
    [SerializeField] private InputActionReference _verticalMovementValue;
    [SerializeField] private InputActionReference _sideStepValue;
    [SerializeField] private InputActionReference _jumpButton;

    [SerializeField] private InputActionReference _escapeButton;
    [SerializeField] private InputActionReference _screenModeButton;
    [SerializeField] private InputActionReference _helpModeButton;
    [SerializeField] private InputActionReference _quickSaveButton;
    [SerializeField] private InputActionReference _povModeButton;

    private void Awake()
    {
        _playerScript = GetComponent<Character>();
        _cameraScript = Camera.main.GetComponent<CameraManager>();
        _menuControls = GetComponent<MenuControls>();
    }

    private void OnEnable()
    {
        _horizontalMovementValue.action.started += HorizontalMovement;
        _horizontalMovementValue.action.canceled += HorizontalMovement;

        _verticalMovementValue.action.started += VerticalMovement;
        _verticalMovementValue.action.canceled += VerticalMovement;

        _sideStepValue.action.started += SideStep;
        _sideStepValue.action.canceled += SideStep;

        _jumpButton.action.started += Jump;
        _escapeButton.action.started += EscapeButton;
        _screenModeButton.action.started += ScreenMode;
        _helpModeButton.action.started += HelpMode;
        _quickSaveButton.action.started += QuickSave;
        _povModeButton.action.started += PovMode;
    }

    private void OnDisable()
    {
        _horizontalMovementValue.action.started -= HorizontalMovement;
        _horizontalMovementValue.action.canceled -= HorizontalMovement;

        _verticalMovementValue.action.started -= VerticalMovement;
        _verticalMovementValue.action.canceled -= VerticalMovement;

        _sideStepValue.action.started -= SideStep;
        _sideStepValue.action.canceled -= SideStep;

        _jumpButton.action.started -= Jump;
        _escapeButton.action.started -= EscapeButton;
        _screenModeButton.action.started -= ScreenMode;
        _helpModeButton.action.started -= HelpMode;
        _quickSaveButton.action.started -= QuickSave;
        _povModeButton.action.started -= PovMode;
    }

    private void HorizontalMovement(InputAction.CallbackContext context)
    {
        _playerScript.Movements.x = context.ReadValue<float>();
    }

    private void VerticalMovement(InputAction.CallbackContext context)
    {
        _playerScript.Movements.y = context.ReadValue<float>();
    }

    private void SideStep(InputAction.CallbackContext context)
    {
        _playerScript.SideStep = context.ReadValue<float>();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _playerScript.Jump();
    }

    private void EscapeButton(InputAction.CallbackContext context)
    {
        _menuControls.OpenMenu();
    }

    private void ScreenMode(InputAction.CallbackContext context)
    {
        // Switch between fullscreen and windowed mode
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void HelpMode(InputAction.CallbackContext context)
    {
        // Toggle/Untoggle help mode
        // Tutorial/Advice and not just a display of the different keys
        Debug.Log("Help Key");
    }

    private void QuickSave(InputAction.CallbackContext context)
    {
        // Quick save only - Do not open the save sub-menu
        Debug.Log("Quick Save Key");
    }

    private void PovMode(InputAction.CallbackContext context)
    {
        // Switch between 3rd (default) and 1st person POV
        _cameraScript.SwitchCameraMode();
    }
}
