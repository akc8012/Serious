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
		Transform startPoint = transform;
		float dist = Vector3.Distance(transform.position, target.position);
		float duration = dist / speed;
		float t = 0;

		while (Vector3.Distance(transform.position, target.position) > 0.01f)
		{
			t += Time.deltaTime / duration;
			transform.position = Vector3.Lerp(startPoint.position, target.position, t);
			transform.rotation = Quaternion.Slerp(startPoint.rotation, target.rotation, t);
			yield return null;
		}

		transform.position = target.position;
		transform.rotation = target.rotation;
		isMoving = false;
	}
}
