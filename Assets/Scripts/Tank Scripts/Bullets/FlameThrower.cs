using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			collision.gameObject.GetComponent<Enemy>().Damage(30);
			collision.gameObject.GetComponent<DamageableOverTime>().Hurt();
		}
	}
}
