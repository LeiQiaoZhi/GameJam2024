using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLaserGun : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Laser laser;
    
    private InputAction shootAction_;
    
    void Start()
    {
        shootAction_ = inputController.GetShootAction();
        shootAction_.performed += ctx => StartShoot();
        shootAction_.canceled += ctx => EndShoot();
    }

    private void EndShoot()
    {
        laser.TurnOff();
    }

    private void StartShoot()
    {
        laser.TurnOn();
    }
}
