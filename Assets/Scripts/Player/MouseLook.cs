using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;
	public bool lockCursor = true;
	
	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;
	private bool m_cursorIsLocked = true;

	private bool canLook = true;
	public bool CanLook { get { return canLook; } }

	private float m_CharacterLastRot;
	private float m_CharacterDeltaRot;
	public float DeltaRotation { get { return m_CharacterDeltaRot; } }

	public void Start()
	{
		Init(transform.root, transform);
	}

	public void Init(Transform character, Transform camera)
	{
		m_CharacterTargetRot = character.localRotation;
		m_CameraTargetRot = camera.localRotation;
	}

	void Update()
	{
		if (canLook)
			LookRotation(transform.root, transform);
	}

	void FixedUpdate()
	{
		if (canLook)
			UpdateCursorLock();
	}

	public void LookRotation(Transform character, Transform camera)
	{
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

		if (clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

		m_CharacterDeltaRot = m_CharacterTargetRot.eulerAngles.y - m_CharacterLastRot;

		if (Mathf.Abs(m_CharacterDeltaRot) > 35)
			m_CharacterDeltaRot = 0;

		m_CharacterLastRot = m_CharacterTargetRot.eulerAngles.y;

		if (smooth)
		{
			character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
				smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot,
				smoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = m_CharacterTargetRot;
			camera.localRotation = m_CameraTargetRot;
		}

		UpdateCursorLock();
	}

	public void UpdateCursorLock()
	{
		//if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate();
	}

	private void InternalLockUpdate()
	{
		if (Input.GetKeyUp(KeyCode.BackQuote) && 
			(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
		{
			m_cursorIsLocked = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (!m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	public void SetCanLook(bool enable)
	{
		canLook = enable;

		Cursor.lockState = enable ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !enable;
	}
}