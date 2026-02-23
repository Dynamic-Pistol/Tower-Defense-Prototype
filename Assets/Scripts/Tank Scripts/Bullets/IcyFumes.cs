using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcyFumes : MonoBehaviour
{

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage(10);
            collision.gameObject.GetComponent<Freezeable>()?.Freeze();
        }
    }
}
