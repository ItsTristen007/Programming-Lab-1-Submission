using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputManager 
{

    private static GameControls _gameControls;


    public static void Init(Player myPlayer)
    {
        _gameControls = new GameControls();

        _gameControls.Permanent.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        

        _gameControls.InGame.Movement.performed += ctx => 
        {

            
            myPlayer.SetMovementDirection(ctx.ReadValue<Vector3>());
        };
        
        _gameControls.InGame.Jump.started += hi =>
        {
            myPlayer.jump();
        };


        _gameControls.InGame.Look.performed += ctx =>
        {
            myPlayer.SetLookRotation(ctx.ReadValue<Vector2>());
        };


        _gameControls.InGame.Shoot.performed += ctx =>
        {
            myPlayer.Shoot();
        };
    }
    


    public static void SetGameControls()
    {
        _gameControls.InGame.Enable();

        _gameControls.UI.Disable();

    }


    public static void SetUIControls()
    {

        _gameControls.UI.Enable();

        _gameControls.InGame.Disable();


    }
}

