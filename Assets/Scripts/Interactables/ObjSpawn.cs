using UnityEngine;
using System.Collections;

public class ObjSpawn : MonoBehaviour
{
    public GameObject[] spawnable;

    void Start ()
    {
        int rnd = Random.Range(0, spawnable.Length);

        Instantiate(spawnable[rnd], transform.position, transform.rotation);
    }
	
	void Update ()
    {
	
	}
}
