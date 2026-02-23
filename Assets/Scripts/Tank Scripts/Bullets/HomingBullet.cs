using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
	[SerializeField]
	private float Range = 1;
	[SerializeField]
	private LayerMask EnemyLayer;

	private void Update()
	{
		var enemy = Physics2D.OverlapCircle(transform.position, Range, EnemyLayer);
		if (enemy != null)
			transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, 25 * Time.deltaTime);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}
