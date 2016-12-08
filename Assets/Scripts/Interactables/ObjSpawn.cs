using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
	[SerializeField]
	GameObject[] spawnable;
	[SerializeField]
	Moveable spawnFrame;

	void Start()
	{
		int ifSpawn = Random.Range(0, 4);

		if (ifSpawn >= 2)
		{
			int rnd = Random.Range(0, spawnable.Length);

			GameObject obj = (GameObject)Instantiate(spawnable[rnd], transform.position, transform.rotation);
			obj.GetComponent<Selectable>().SetFloorPoint(transform.position);
			obj.transform.parent = transform.parent;

			if (spawnFrame)
			{
				spawnFrame.SetBehindFrame(obj);
				obj.SetActive(false);
			}
		}
	}

	void Update()
	{

	}
}
