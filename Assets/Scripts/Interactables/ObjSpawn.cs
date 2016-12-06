using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
    const int spawnNum = 2;
    public GameObject[] spawnable = new GameObject[spawnNum];

    void Start ()
    {
        int rnd = Random.Range(0, spawnNum);

        Instantiate(spawnable[rnd], transform.position, transform.rotation);
    }
	
	void Update ()
    {
	
	}
}
