using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
	public LayerMask movementMask;
	Camera cam;
	PlayerMotor motor;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}

	// Update is called once per frame
	void Update () {
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
			}
		}
	}
}
