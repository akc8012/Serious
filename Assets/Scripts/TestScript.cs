using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour
{
	
	void Start()
	{
		print("This is a test");
	}

	void Update()
	{
		transform.position += Vector3.forward * Time.deltaTime;
	}
}