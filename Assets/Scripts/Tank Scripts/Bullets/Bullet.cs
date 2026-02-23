using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected int Damage = 0;
    protected bool Freezes;
    [SerializeField]
    private bool CrypticBullet = false;
    [SerializeField]
    private bool DamageOverTime = false;

    public void Init(int damage,bool freezes)
    {
        Damage = damage;
        Freezes = freezes;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageEnemy(collision.gameObject);
        }
    }

    protected void DamageEnemy(GameObject enemy)
	{
		enemy.GetComponent<Enemy>().Damage(Damage);
        if (Freezes)
            enemy.GetComponent<Freezeable>()?.Freeze();
        if (CrypticBullet)
            enemy.GetComponent<Freezeable>()?.Crypt();
        if (DamageOverTime)
            enemy.GetComponent<DamageableOverTime>()?.Hurt();
		SelfDestroy();
	}

    private void OnBecameInvisible()
    {
        if (transform.position.x <= -20.25f || transform.position.y <= -13 || transform.position.x >= 20 || transform.position.y >= 12)
        {
            SelfDestroy();
        }        
    }

    protected void SelfDestroy()
	{
		Destroy(gameObject);
	}
}
