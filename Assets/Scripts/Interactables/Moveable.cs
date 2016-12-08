using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour
{
	[SerializeField]
	bool modifyScale = true;

	struct Point
	{
		public Vector3 position;
		public Vector3 lossyScale;
		public Quaternion rotation;

		public Point(Vector3 position, Vector3 lossyScale, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
			this.lossyScale = lossyScale;
		}
	}

	Point startPoint;
	Point endPoint;
	bool isMoving = false;
	public bool IsMoving { get { return isMoving; } }

	enum Points { Start, End };
	Points nextPoint = Points.End;

	[SerializeField]
	float speed = 2;

	void Start()
	{
		startPoint = new Point(transform.position, transform.lossyScale, transform.rotation);
		Transform endPointRef = transform.Find("EndPoint");
		endPoint = new Point(endPointRef.position, endPointRef.lossyScale, endPointRef.rotation);
	}

	public void MoveToNext()
	{
		if (isMoving) return;

		if (nextPoint == Points.End)
			MoveToEnd();
		else
			MoveToStart();
	}

	public void MoveToEnd()
	{
		if (isMoving) return;
		StartCoroutine(MoveTo(endPoint));
		nextPoint = Points.Start;
	}

	public void MoveToStart()
	{
		if (isMoving) return;
		StartCoroutine(MoveTo(startPoint));
		nextPoint = Points.End;
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
			if (modifyScale) transform.localScale = Vector3.Lerp(startPoint.lossyScale, target.lossyScale, t);
			transform.rotation = Quaternion.Slerp(startPoint.rotation, target.rotation, t);
			yield return null;
		}

		transform.position = target.position;
		transform.rotation = target.rotation;
		isMoving = false;
	}
}
