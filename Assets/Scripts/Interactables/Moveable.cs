using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour
{
	struct Point
	{
		public Vector3 position;
		public Quaternion rotation;

		public Point(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		public static bool Compare(Point a, Point b)
		{
			return (a.position == b.position && a.rotation == b.rotation);
		}
	}

	Point startPoint;
	Point endPoint;
	Point nextPoint;
	bool isMoving = false;
	public bool IsMoving { get { return isMoving; } }

	[SerializeField]
	float speed = 2;

	void Start()
	{
		startPoint = new Point(transform.position, transform.rotation);
		Transform endPointRef = transform.Find("EndPoint");
		endPoint = new Point(endPointRef.position, endPointRef.rotation);
		nextPoint = endPoint;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			MoveToEnd();
		if (Input.GetKeyDown(KeyCode.P))
			MoveToStart();
	}

	public void MoveToNext()
	{
		if (isMoving) return;

		if (Point.Compare(nextPoint, endPoint))
			MoveToEnd();
		if (Point.Compare(nextPoint, startPoint))
			MoveToStart();
	}

	public void MoveToEnd()
	{
		if (isMoving) return;
		StartCoroutine(MoveTo(endPoint));
		nextPoint = startPoint;
	}

	public void MoveToStart()
	{
		if (isMoving) return;
		StartCoroutine(MoveTo(startPoint));
		nextPoint = endPoint;
	}

	IEnumerator MoveTo(Point target)
	{
		isMoving = true;
		while (Vector3.Distance(transform.position, target.position) > 0.001f)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
			yield return null;
		}

		transform.position = target.position;
		isMoving = false;
	}
}