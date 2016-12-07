using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
    public GameObject[] spawnable;

    void Start ()
    {
        int rnd = Random.Range(0, spawnable.Length);

        GameObject obj = (GameObject)Instantiate(spawnable[rnd], transform.position, transform.rotation);
		obj.GetComponent<Selectable>().SetFloorPoint(transform.position);
    }
	
	void Update ()
    {
	
	}
}
