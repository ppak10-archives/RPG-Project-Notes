using UnityEngine;

public class Interactable : MonoBehaviour {
	public float radius = 3f; // how close player needs to get
	public Transform interactionTransform;

	bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	public virtual void Interact () // Virtual method derived from this baseclass
	{
		// This method is meant to be overwritten
		Debug.Log("Interacting: "+ transform.name);
	}

	void Update()
	{
		if (isFocus &&!hasInteracted)
		{
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			if (distance <= radius)
			{
				Debug.Log("INTERACT");
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused (Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused ()
	{
		isFocus = false;
		player = null;
		hasInteracted = false;
	}

	void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}
}
