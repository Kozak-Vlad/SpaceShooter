using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System;

/// <summary>
/// Handles player specific behaviors such as moving, shooting and taking damage
/// </summary>
public class Player : Ship {


    public float BackgroundMultiplyer = 0.008f;
    public UserInterfaceManager UserInterface;
    public InputActionReference move, shoot;
    private Vector2 direction;
    public EnvrionmentManager EnvironmentMngr;
    private void Start()
    {
        shoot.action.started += Shoot;
    }

    // Update is called once per frame
    void Update () {
        direction = move.action.ReadValue<Vector2>();
        if ( direction.x < 0) //Move left
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.left, Speed * Time.deltaTime);
            EnvironmentMngr.SetMovingHorizontalMultiplyer(-BackgroundMultiplyer);
        }
        else if (direction.x > 0) //Move Right
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.left, Speed * Time.deltaTime);
            EnvironmentMngr.SetMovingHorizontalMultiplyer(BackgroundMultiplyer);
        }

        else
        {
            EnvironmentMngr.SetMovingHorizontalMultiplyer(0);
        }

        if (direction.y > 0) //Move up
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward, Speed * Time.deltaTime);
            EnvironmentMngr.SetMovingVerticallyMultiplyer(BackgroundMultiplyer);

        }
        else if (direction.y < 0) //Move down
        {
            transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.forward, Speed * Time.deltaTime);
            EnvironmentMngr.SetMovingVerticallyMultiplyer(-BackgroundMultiplyer);
        }

        else
        {
            EnvironmentMngr.SetMovingVerticallyMultiplyer(0);
        }
    }
    private void Shoot(InputAction.CallbackContext ctx) 
    {
        if (!CanShoot) return;
        CanShoot = false;
        ShootLaser();
    }


    public override void CalculateHit(int amount)
    {
        //Remove heart from UI
        UserInterface.DecreasePlayerHealth();
        base.CalculateHit(amount);

        if (HitPoints <= 0)
        {
            GameMngr.EndGame(); 
        }
    }

}
