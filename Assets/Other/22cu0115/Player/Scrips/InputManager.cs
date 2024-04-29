using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Key
{
    KeybordMoveUp,
    KeybordMoveDown,
    KeybordMoveLeft,
    KeybordMoveRight,
    KeybordJump,
    GamepadMoveLeft,
    GamepadMoveRight,
    GamepadJump
}

public class InputManager : MonoBehaviour
{
    // Keybord
    private static bool keybordMoveUp;
    private static bool keybordMoveDown;
    private static bool keybordMoveLeft;
    private static bool keybordMoveRight;
    private static bool keybordJump;
    // Gamepad
    private static bool gamepadMoveLeft;
    private static bool gamepadMoveRight;
    private static Vector3 gamepadMoveAmount;
    private static bool gamepadJump;

    private void Update()
    {
        // Keybord
        if (Keyboard.current != null)
        {
            keybordMoveUp = Keyboard.current.wKey.isPressed;
            keybordMoveDown = Keyboard.current.sKey.isPressed;
            keybordMoveLeft = Keyboard.current.aKey.isPressed;
            keybordMoveRight = Keyboard.current.dKey.isPressed;
            keybordJump = Keyboard.current.spaceKey.wasPressedThisFrame;
        }
        // Gamepad
        if (Gamepad.current != null)
        {
            gamepadMoveLeft = Gamepad.current.leftStick.left.IsActuated();
            gamepadMoveRight = Gamepad.current.leftStick.right.IsActuated();
            gamepadMoveAmount = Gamepad.current.leftStick.value;
            gamepadJump = Gamepad.current.buttonSouth.wasPressedThisFrame;
        }
    }

    public static bool GetKey(Key key)
    {
        switch (key)
        {
            // Kerybord
            case Key.KeybordMoveUp:
                return keybordMoveUp;
            case Key.KeybordMoveDown:
                return keybordMoveDown;
            case Key.KeybordMoveLeft:
                return keybordMoveLeft;
            case Key.KeybordMoveRight:
                return keybordMoveRight;
            case Key.KeybordJump:
                return keybordJump;
            // Gamepad
            case Key.GamepadMoveLeft:
                return gamepadMoveLeft;
            case Key.GamepadMoveRight:
                return gamepadMoveRight;
            case Key.GamepadJump:
                return gamepadJump;
        }
        return false;
    }

    public static Vector2 GetMoveValue()
    {
        return gamepadMoveAmount;
    }
}