using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	public LayerMask movementMask;
	public Interactable focus;
	Camera cam;
	PlayerMotor motor;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}

	// Update is called once per frame
	void Update () {
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100, movementMask))
			{
				Debug.Log("Left-clicked: "+hit.collider.name+" "+hit.point);
				motor.MoveToPoint(hit.point);
				// Move our player to what we hit

				// Stop focusing any objects
				RemoveFocus();
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				Debug.Log("Right-clicked: "+hit.collider.name+" "+hit.point);
				// Check if there is interactable but have it focus
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}
	}

	void SetFocus (Interactable newFocus)
	{
		if (newFocus!= focus)
		{
			if (focus != null)
				focus.OnDefocused();

			focus = newFocus;
			motor.FollowTarget(newFocus);
		}
		newFocus.OnFocused(transform);

	}

	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
		motor.StopFollowingTarget();
	}
}
