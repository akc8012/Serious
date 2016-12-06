using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
    const int spawnNum = 2;
    public GameObject[] spawnable = new GameObject[spawnNum];

    void Start ()
    {
        int rnd = Random.Range(0, spawnNum);

        if (rnd == 0)
        {
            Instantiate(spawnable[0], transform.position, transform.rotation);
        }

        if (rnd == 1)
        {
            Instantiate(spawnable[1], transform.position, transform.rotation);
        }
    }
	
	void Update ()
    {
	
	}
}
