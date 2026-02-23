using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShootersController : MonoBehaviour
{
    [SerializeField]
    private LayerMask Enemylayer;
    [SerializeField]
    private StaticShoot[] shooters;
    [SerializeField]
    private float Range = 3;
    [SerializeField]
    private int Damage = 14;
    [SerializeField]
    private float Firerate = 0.5f;
    private float Nextfire = 0;

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, Range,Enemylayer))
        {
            if (Time.time > Nextfire)
            {
                Nextfire = Time.time + Firerate;
				foreach (var shooter in shooters)
				{
					shooter.Shoot(Damage);
				}
			}
        }
    }
}
