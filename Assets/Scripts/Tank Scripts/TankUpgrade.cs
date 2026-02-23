using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade",menuName = "Tank-droids/Tank Upgrade")]
public class TankUpgrade : ScriptableObject 
{
	public GameObject Upgrade;
	public Sprite Icon;
	public int Cost;
}
