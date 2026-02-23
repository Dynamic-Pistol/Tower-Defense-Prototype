using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShoot : MonoBehaviour
{
	[SerializeField]
	private GameObject Bullet;
	[SerializeField]
	private Transform FirePoint;
	private AudioSource firepointaudiosource;

	private void Start()
	{
		firepointaudiosource = FirePoint.GetComponent<AudioSource>();
	}

	public void Shoot(int damage)
	{
		GameObject bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
		firepointaudiosource.Play();
		bullet.GetComponent<Bullet>().Init(damage, false);
		bullet.GetComponent<Rigidbody2D>().AddForce(FirePoint.up * 10, ForceMode2D.Impulse);
	}
}
