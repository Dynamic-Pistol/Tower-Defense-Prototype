using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField]
    private int ReganAmount;
    private Enemy self;

    private void Start()
    {
        self = GetComponent<Enemy>();
        InvokeRepeating(nameof(Regan), 1, 1f);
    }

    private void Regan()
    {
        self.Heal(ReganAmount);
    }
}
