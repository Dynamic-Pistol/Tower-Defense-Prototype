using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [Header("Gameobjects and Components")]
    [SerializeField]
    private GameObject DeathEffect;
    [SerializeField]
    private Transform HealthBar;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private AudioSource hurtaudiosource;
    [Header("Health")]
    [SerializeField]
    private int MaxHealth = 10;
    [SerializeField]
    private int Defense;
    [SerializeField]
    private int Worth = 5;
    private int Health;
	private float Iframe = 0;
	[Header("Movement")]
    [SerializeField]
    private float Speed;
	private Transform[] Waypoints;
	private int WaypointIndex;
    public bool IsSlown { private get; set; }
    public bool IsFrozen { private get; set; }
    public bool IsHurt { private get; set; }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        hurtaudiosource = GetComponent<AudioSource>();
        Health = MaxHealth;
        Transform waypointsparent = GameObject.Find("Waypoints").transform;
        Waypoints = new Transform[waypointsparent.childCount];
        for (int i = 0; i < waypointsparent.childCount ; i++)
        {
            Waypoints[i] = waypointsparent.GetChild(i);
        }
    }

    private void Update()
    {
        if (WaypointIndex == Waypoints.Length)
		{
            sr.DOKill();
			HealthBar.DOKill();
			Destroy(gameObject);
			GameManager.Instance.DealDamage();
		}
        else if (transform.position == Waypoints[WaypointIndex].position)
            WaypointIndex++;
        else
            rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, Waypoints[WaypointIndex].position,IsFrozen ? 0 : (Time.deltaTime * (IsSlown ? Speed / 2 : Speed))));
        if (IsSlown)
            sr.DOBlendableColor(Color.blue, 0.1f);
        else
            sr.DOBlendableColor(Color.white, 0.1f);
        if (IsHurt)
            Damage(Mathf.Clamp(MaxHealth / 10,1,int.MaxValue));
    }

    public void Damage(int damage)
    {
        IEnumerator DamageColorChange()
        {
            sr.DOBlendableColor(Color.red, 0.25f);
            yield return new WaitForSeconds(0.5f);
			sr.DOBlendableColor(Color.white, 0.25f);
		}
        if (Time.time > Iframe)
        {
            Iframe = Time.time + 0.75f;
            Health -= Mathf.Clamp(damage - Defense, 0, int.MaxValue);
            if (Health <= 0)
            {
                Instantiate(DeathEffect, transform.position, Quaternion.identity);
                sr.DOKill();
                HealthBar.DOKill();
                GameManager.Instance.AddMoney(Worth);
                Destroy(gameObject);
            }
            else
			{
				StartCoroutine(DamageColorChange());
                HealthBar.DOScaleX((float)Health / MaxHealth, 0.5f);
                hurtaudiosource.Play();
			}
        }
    }

    public void Heal(int HealAmount)
	{
        if (Health == MaxHealth)
            return;
		IEnumerator HealColorChange()
		{
			sr.DOBlendableColor(Color.green, 0.25f);
			yield return new WaitForSeconds(0.5f);
			sr.DOBlendableColor(Color.white, 0.25f);
		}
		Health = Mathf.Clamp(Health + HealAmount, 0, MaxHealth);
        StartCoroutine(HealColorChange());
		HealthBar.DOScaleX((float)Health / MaxHealth, 0.5f);
	}
}
