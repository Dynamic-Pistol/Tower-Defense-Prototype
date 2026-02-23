using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;

public class PrefabScreenShotter : MonoBehaviour
{
	[SerializeField]
	private GameObject[] prefabs;


	private void Start()
	{
		Screenshot();
	}

	public void Screenshot()
	{
		foreach (GameObject go in prefabs)
		{
			DoSnapshot(go, Camera.main);
		}
	}

	private void DoSnapshot(GameObject go, Camera cam)
	{
		var ins = GameObject.Instantiate(go, new Vector3(), Quaternion.identity);

		ins.SetActive(true);

		string fileName = go.name + ".png";
		string astPath = "Assets/Prefabs/snapshots/" + fileName;
		fileName = Application.dataPath + "/Prefabs/snapshots/" + fileName;
		FileInfo info = new FileInfo(fileName);
		if (info.Exists)
			File.Delete(fileName);
		else if (!info.Directory.Exists)
			info.Directory.Create();

		var renderTarget = RenderTexture.GetTemporary(600, 600);
		cam.targetTexture = renderTarget;
		cam.Render();

		RenderTexture.active = renderTarget;
		Texture2D tex = new Texture2D(renderTarget.width, renderTarget.height);
		tex.ReadPixels(new Rect(0, 0, renderTarget.width, renderTarget.height), 0, 0);

		File.WriteAllBytes(fileName, tex.EncodeToPNG());

		cam.targetTexture = null;
		DestroyImmediate(ins);
	}
}