using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;


    public float MoveSpeed;
    public float BrakeForce;
    public float TorqueForce;
    public bool OnGround;

    public Transform StartPos;
    public int Distance;

    public Text DistUi;

    [SerializeField] private AudioSource TractSfx;
    public Scrollbar fuel;




    private void FixedUpdate()
    {
        if (GameManager.gm.gameStarted) 
        {
            Distance = (int)Vector3.Distance(this.gameObject.transform.position, StartPos.position);
            DistUi.text=Distance.ToString();
            if (Input.GetKeyDown(KeyCode.D))
            {
                StopAllCoroutines();
                StartCoroutine(EnginePitchUp());
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (OnGround)
                {
                    MoveTo(Vector3.right, MoveSpeed);
                    DropFuel(0.001f);
                }
                else
                {
                    TorqueTo(-TorqueForce);
                    DropFuel(0.001f);
                }
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                StopAllCoroutines();
                StartCoroutine(EnginePitchDown());
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (OnGround)
                {
                    if (rb.velocity.x > 0)
                    {
                        MoveTo(Vector3.left, BrakeForce);
                    }
                }
                else
                {
                    TorqueTo(TorqueForce);
                    DropFuel(0.001f);

                }
            }
        }
    }


    public void MoveTo(Vector3 direction, float speed)
    {
        rb.AddForce(direction * speed,ForceMode2D.Force);
    }

    public void TorqueTo(float dir)
    {
        rb.AddTorque(dir, ForceMode2D.Force);
    }

    public void DropFuel(float amount)
    {
        fuel.size -= amount;
        if (fuel.size <= 0f)
        {
            if (GameManager.gm.gameStarted)
            {
                rb.velocity = Vector3.zero;
                GameManager.gm.gameStarted = false;
                GameManager.gm.GameOver();
                GameManager.gm.GameOverDistTxt.text = Distance.ToString();

                if (PlayerPrefs.GetInt("BestDistance") < Distance)
                {
                    PlayerPrefs.SetInt("BestDistance", Distance);
                }
            }
          
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Canister"))
        {
            fuel.size = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.gm.gameStarted)
            {
                rb.velocity = Vector3.zero;
                GameManager.gm.gameStarted = false;
                GameManager.gm.GameOver();
                GameManager.gm.GameOverDistTxt.text = Distance.ToString();

                if (PlayerPrefs.GetInt("BestDistance") < Distance)
                {
                    PlayerPrefs.SetInt("BestDistance", Distance);
                }
            }

        }
    }

    public IEnumerator EnginePitchUp()
    {
        while (TractSfx.pitch < 1.8)
        {
            TractSfx.pitch += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator EnginePitchDown()
    {
        while (TractSfx.pitch >= 1f)
        {
            TractSfx.pitch -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

 
}
