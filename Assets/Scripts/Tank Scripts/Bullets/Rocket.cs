using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private LayerMask Enemylayer;
    [SerializeField]
    private float Range = 5;
    [SerializeField]
    private GameObject ExplodeEffect;
    [SerializeField]
    private AudioClip explosionsfx;

    private void OnDestroy()
    {
        if (Application.isPlaying == true)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2.5f,Enemylayer);
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Enemy>().Damage(10);
            }
            Instantiate(ExplodeEffect, transform.position, Quaternion.identity).AddComponent<AudioSource>().PlayOneShot(explosionsfx);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
