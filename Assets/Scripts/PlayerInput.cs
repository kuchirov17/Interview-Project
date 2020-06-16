using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool gas, brake;
    [SerializeField] private GameObject gasObj, brakeObj;
    [SerializeField] private PlayerMovement PlayerMovement;
 
    private void OnMouseDown()
    {
        if (GameManager.gm.gameStarted)
        {
            if (gas)
            {
                gasObj.transform.position += new Vector3(0, -0.2f);
                PlayerMovement.StopAllCoroutines();
                PlayerMovement.StartCoroutine(PlayerMovement.EnginePitchUp());
            }
            if (brake)
            {
                brakeObj.transform.position += new Vector3(0, -0.2f);
                PlayerMovement.StopAllCoroutines();
                PlayerMovement.StartCoroutine(PlayerMovement.EnginePitchDown());
            }
        }
        
    }

    private void OnMouseUp()
    {
        if (GameManager.gm.gameStarted)
        {

            if (gas)
            {
                gasObj.transform.position += new Vector3(0, 0.2f);
                PlayerMovement.StopAllCoroutines();
                PlayerMovement.StartCoroutine(PlayerMovement.EnginePitchDown());
            }
            if (brake)
            {
                brakeObj.transform.position += new Vector3(0, 0.2f);
                PlayerMovement.StopAllCoroutines();

            }
        }
        
    }

    private void OnMouseDrag()
    {
        if (GameManager.gm.gameStarted)
        {
            if (gas)
            {
                if (PlayerMovement.OnGround)
                {

                    PlayerMovement.MoveTo(Vector3.right, PlayerMovement.MoveSpeed);
                    PlayerMovement.DropFuel(0.001f);
                }
                else
                {
                    PlayerMovement.TorqueTo(    PlayerMovement.TorqueForce);
                    PlayerMovement.DropFuel(0.001f);
                }
            }

            if (brake)
            {
                if (PlayerMovement.OnGround)
                {
                    if (PlayerMovement.rb.velocity.x > 0)
                    {
                        PlayerMovement.MoveTo(Vector3.left, PlayerMovement.BrakeForce);
                        PlayerMovement.DropFuel(0.001f);
                    }
                }
                else
                {
                    PlayerMovement.TorqueTo(-PlayerMovement.TorqueForce);
                    PlayerMovement.DropFuel(0.001f);
                }
            }
        }
        
   
    }
}
