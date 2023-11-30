using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager
{
    private static GameControl _Controls;

    public static void Init(Player myplayer)
    {
        _Controls = new GameControl();
        
        _Controls.Game.Movement.performed += hi =>
        {
            myplayer.SetMovementDirection(hi.ReadValue<Vector3>());
        };

        _Controls.Game.Jump.started += hello =>
        {
            myplayer.Jump();
        };

        _Controls.Game.Look.performed += ctx =>
        {
            myplayer.SetLookRotation(ctx.ReadValue<Vector2>());
        };

        _Controls.Game.Shoot.started += ctx =>
        {
            myplayer.Shoot();
        };
        
        _Controls.Game.Reload.started += hello =>
        {
            myplayer.Reload();
        };
        
        _Controls.Game.Spawn.started += hello =>
        {
            myplayer.ResetToCheckPoint();
        };
        
        _Controls.Permanent.Enable();
    }

    public static void SetGameControls()
    {
        _Controls.Game.Enable();
        _Controls.UI.Disable();
    }

    public static void SetUIControls()
    {
        _Controls.UI.Enable();
        _Controls.Game.Disable();
    }
}