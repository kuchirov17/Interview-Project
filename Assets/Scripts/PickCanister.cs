using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickCanister : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource CanisterSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanisterSFX.Play();
            anim.SetBool("Animate", true);
            StartCoroutine(DestroyAfter(this.gameObject, 2f));
            
        }
    }

    IEnumerator DestroyAfter(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
