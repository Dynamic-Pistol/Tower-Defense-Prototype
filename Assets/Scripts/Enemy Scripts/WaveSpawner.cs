using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
	private EnemyWaveSO[] waves;
	private List<GameObject> currentwaveenemies;
	private int currentwave = 0;


    private void Start()
    {
        StartCoroutine(SummonEnemies());
    }

    IEnumerator SummonEnemies()
	{
		currentwaveenemies = new List<GameObject>();
		yield return new WaitForSeconds(5);
        foreach (var enemytospawn in waves[currentwave].EnemyWave)
        {
            var go = Instantiate(enemytospawn,transform.position,Quaternion.identity);
            currentwaveenemies.Add(go);
            yield return new WaitForSeconds(1f);
		}
        while (currentwaveenemies.Count != 0)
        {
            for (int i = 0; i < currentwaveenemies.Count; i++)
            {
                if (currentwaveenemies[i] == null)
                    currentwaveenemies.RemoveAt(i);
            }
			yield return null;
		}
        currentwave++;
        StartCoroutine(SummonEnemies());
    }
}
