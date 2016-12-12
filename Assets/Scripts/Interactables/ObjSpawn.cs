using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
	[SerializeField]
	GameObject[] spawnable;
	[SerializeField]
	Moveable spawnFrame;
	[SerializeField]
	bool randomizeYrotation = true;
	[SerializeField]
	bool insideOutlet = false;
	[SerializeField]
	bool forceSpawn = false;

	void Start()
	{
		int ifSpawn = Random.Range(0, 4);

		if (ifSpawn >= 2 || forceSpawn)
		{
			int rnd = Random.Range(0, spawnable.Length);

			GameObject obj = (GameObject)Instantiate(spawnable[rnd], transform.position, transform.rotation);
			obj.GetComponent<Selectable>().SetFloorPoint(transform.position);
			obj.transform.parent = transform.parent;

			if (obj.GetComponent<Selectable>().IsHarmful)
				ScoreManager.instance.AddHarmfulObject(obj.name);

			if (randomizeYrotation)
				obj.transform.rotation = Quaternion.Euler(obj.transform.rotation.x, Random.Range(0, 360), obj.transform.rotation.z);

			if (spawnFrame)
			{
				spawnFrame.SetBehindFrame(obj);
				obj.SetActive(false);
			}

			if (insideOutlet)
			{
				GameObject.Find("VisibilityTrigger").GetComponent<VisibilityTrigger>().AddToGroupB(obj);
				obj.SetActive(false);
			}
		}
	}
}
