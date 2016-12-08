using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
    public GameObject[] spawnable;

    void Start ()
    {
        int ifSpawn = Random.Range(0, 4);

        if (ifSpawn >= 2)
        {
            int rnd = Random.Range(0, spawnable.Length);

            GameObject obj = (GameObject)Instantiate(spawnable[rnd], transform.position, transform.rotation);
            obj.GetComponent<Selectable>().SetFloorPoint(transform.position);

            obj.transform.parent = GameObject.Find("Selectables").transform;
        }
    }
	
	void Update ()
    {
	
	}
}
