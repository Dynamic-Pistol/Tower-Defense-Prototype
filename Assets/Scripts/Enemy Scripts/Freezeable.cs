using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezeable : MonoBehaviour
{
    private Enemy enemy;
    private float TimetillUnfreeze;
    private float TimetillUncrypt;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        TimetillUnfreeze -= Time.deltaTime;
        TimetillUncrypt -= Time.deltaTime;
        enemy.IsSlown = TimetillUnfreeze > 0;
        enemy.IsFrozen = TimetillUncrypt > 0;
    }

    public void Freeze()
    {
        TimetillUnfreeze = 5;
    }

    public void Crypt()
    {
        TimetillUncrypt = 10;
    }
}
