/*
 * Full Name: Hardik Dipakbhai Shah
 * Student ID : 101249099
 * Date Modified : December 30,2022
 * File : MovementComponent.cs
 * Description : This is MovementComponent Script
 * Revision History : v0.1 > Added PlayerInput using Unity InputAction
 *              
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    [Header("Movement Variables")]
    public float Speed = 5.0f;

    //Components
    PlayerController playerController;

    [Header("Movement Refrences")]
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        print("InputVector : " + inputVector);
    }
}
