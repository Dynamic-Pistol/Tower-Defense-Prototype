using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShoot : MonoBehaviour
{

    [SerializeField]
    private Transform FirePoint;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float Range = 3f;
    [SerializeField]
    private LayerMask EnemyLayer;
    private AudioSource firepointaudiosource;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float firerate = 1;
    private float nextfire = 0;
    [SerializeField]
    private bool Freezes = false;

    private void Start()
    {
        firepointaudiosource = FirePoint.GetComponent<AudioSource>();
    }

	private void Update()
    {
        float GetAngleFromVector2(Vector2 target)
        {
            Vector2 direction = target - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            return angle;
        }
        Transform GetClosestEnemy()
        {
            Transform enemytransform = null;
            var enemies = Physics2D.OverlapCircleAll(transform.position, Range, EnemyLayer);
            float closestdistance = float.PositiveInfinity;
            foreach (var enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
				if (distance < closestdistance) 
                {
                    closestdistance = distance;
                    enemytransform = enemy.transform;
                }
            }
            return enemytransform;
        }
        bool IsEnemyThere()
        {
            return Physics2D.Raycast(FirePoint.position,FirePoint.up * Range,Range,EnemyLayer);
        }
        Transform enemy = GetClosestEnemy();
        if (enemy != null)
		{
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, GetAngleFromVector2(enemy.position)), 100 * Time.deltaTime);
            if (Time.time > nextfire && IsEnemyThere())
			{
				nextfire = Time.time + firerate;
				Shoot();
			}
		}
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, FirePoint.position,FirePoint.rotation);
        firepointaudiosource.Play();
        bullet.GetComponent<Bullet>().Init(damage,Freezes);
        bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * 10, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
