using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
	protected bool startedToOpen = false;
	protected bool startedToClose = true;
	protected GameObject player;

	protected float startAngle;
	protected float openAngle;
	protected bool doorOpened = false;
	protected int[] openAngles = new int[] { 225, 45, 0, 0 };   //these angles correspond to the side enum

	public float openSpeed = 10;
	public enum Side { Right, Left, Front, Back };
	public Side side;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if (side == Side.Right)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, 359.99f, transform.eulerAngles.z);
			openAngle = 230;
		}

		startAngle = transform.eulerAngles.y;
		openAngle = openAngles[(int)side];
	}

	void Update()
	{
		if (Vector3.Distance(transform.position, player.transform.position) < 6f)
		{
			StartCoroutine(OpenDoor());
		}

		if (Vector3.Distance(transform.position, player.transform.position) > 8f && doorOpened)
		{
			StartCoroutine(CloseDoor());
		}
	}

	protected virtual IEnumerator OpenDoor()
	{
		startedToOpen = true;
		startedToClose = false;

		while (transform.eulerAngles.y >= openAngle)
		{
			transform.Rotate(-Vector3.up, Time.deltaTime * openSpeed);
			yield return null;
		}

		doorOpened = true;
	}

	IEnumerator CloseDoor()
	{
		startedToClose = true;

		while (transform.eulerAngles.y <= startAngle && transform.eulerAngles.y >= 10)
		{
			transform.Rotate(Vector3.up, Time.deltaTime * openSpeed);
			yield return null;
		}

		doorOpened = false;
		startedToOpen = false;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle, transform.eulerAngles.z);
	}
}