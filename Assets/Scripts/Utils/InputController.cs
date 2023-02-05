using UnityEngine;
using UnityEngine.InputSystem;

public class InputController
{

    public static float Horizontal
    {
        get
        {
            var keyLeft = Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed;
            var keyRight = Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed;
            var res = 0.0f;
            if (keyLeft)
                res -= 1.0f;
            if (keyRight)
                res += 1.0f;
            return res;
        }
    }

    public static Vector2 LeftStick
    {
        get
        {
            return Gamepad.current == null ? Vector2.zero : Gamepad.current.leftStick.ReadValue();
        }
    }

    public static bool MoveRight(float threshold = 0.2f)
    {
        var keyHor = InputController.Horizontal;
        var padHor = InputController.LeftStick.x;
        return keyHor > threshold || padHor > threshold;
    }

    public static bool MoveLeft(float threshold = 0.2f)
    {
        var absThreshold = Mathf.Abs(threshold);

        var keyHor = InputController.Horizontal;
        var padHor = InputController.LeftStick.x;

        return keyHor < -absThreshold || padHor < -absThreshold;
    }

    public static float Vertical
    {
        get
        {
            var keyUp = Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed;
            var keyDown = Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed;
            var res = 0.0f;
            if (keyUp)
                res += 1.0f;
            if (keyDown)
                res -= 1.0f;
            return res;
        }
    }

    public static bool Up
    {
        get
        {
            var upArrow = Keyboard.current.upArrowKey.isPressed;

            if (Gamepad.current == null)
                return upArrow;
            var gamepadUp = Gamepad.current.dpad.up.isPressed;

            return upArrow || gamepadUp;
        }
    }

    public static bool Right
    {
        get
        {
            var rightArrow = Keyboard.current.rightArrowKey.isPressed;

            if (Gamepad.current == null)
                return rightArrow;
            var gamepadRight = Gamepad.current.dpad.right.isPressed;

            return rightArrow || gamepadRight;
        }
    }

    public static bool Down
    {
        get
        {
            var downArrow = Keyboard.current.downArrowKey.isPressed;

            if (Gamepad.current == null)
                return downArrow;
            var gamepadDown = Gamepad.current.dpad.down.isPressed;

            return downArrow || gamepadDown;
        }
    }

    public static bool Left
    {
        get
        {
            var leftArrow = Keyboard.current.leftArrowKey.isPressed;

            if (Gamepad.current == null)
                return leftArrow;
            var gamepadLeft = Gamepad.current.dpad.left.isPressed;

            return leftArrow || gamepadLeft;
        }
    }

    public static bool Jump
    {
        get
        {
            var keyJump = Keyboard.current.zKey.isPressed;
            if (Gamepad.current == null)
                return keyJump;
            var gamePadJump = Gamepad.current.buttonSouth.isPressed;
            return keyJump || gamePadJump;
        }
    }

    public static bool JumpDown
    {
        get
        {
            var keyJump = Keyboard.current.zKey.wasPressedThisFrame;
            if (Gamepad.current == null)
                return keyJump;
            var gamePadJump = Gamepad.current.buttonSouth.wasPressedThisFrame;
            return keyJump || gamePadJump;
        }
    }

    public static bool DashDown
    {
        get
        {
            var keyDash = Keyboard.current.xKey.wasPressedThisFrame;
            if (Gamepad.current == null)
                return keyDash;
            var gamePadDash = Gamepad.current.buttonWest.wasPressedThisFrame;
            return keyDash || gamePadDash;
        }
    }

    public static bool W
    {
        get { return Keyboard.current.wKey.isPressed; }
    }

    public static bool D
    {
        get { return Keyboard.current.dKey.isPressed; }
    }

    public static bool S
    {
        get { return Keyboard.current.sKey.isPressed; }
    }

    public static bool A
    {
        get { return Keyboard.current.aKey.isPressed; }
    }

    public static bool Q
    {
        get { return Keyboard.current.f12Key.isPressed; }
    }

    public static bool E
    {
        get { return Keyboard.current.eKey.isPressed; }
    }


    public static bool Z
    {
        get { return Keyboard.current.zKey.isPressed; }
    }

    public static bool Restart
    {
        get { return Keyboard.current.rKey.isPressed; }
    }

    public static bool Undo
    {
        get { return InputController.Z; }
    }

    public static bool Return
    {
        get { return Keyboard.current.enterKey.wasPressedThisFrame; }
    }

    public static bool ResetGame
    {
        get { return Keyboard.current.f5Key.wasPressedThisFrame; }
    }

}