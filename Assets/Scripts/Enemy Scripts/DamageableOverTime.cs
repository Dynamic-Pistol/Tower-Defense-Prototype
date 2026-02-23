using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableOverTime : MonoBehaviour
{
    private Enemy enemy;
    private float TimetillDamageEnd = 0;

	private void Start()
	{
		enemy = GetComponent<Enemy>();
	}

	private void Update()
	{
		TimetillDamageEnd -= Time.deltaTime;
		enemy.IsHurt = TimetillDamageEnd > 0;
	}

	public void Hurt()
	{
		TimetillDamageEnd = 5;
	}
}
