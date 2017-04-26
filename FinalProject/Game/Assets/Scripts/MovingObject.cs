using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovingObject : MonoBehaviour
{
	public float moveTime = 0.1f;			//Time it will take object to move, in seconds.

	public LayerMask blockingLayer;			//Layer on which collision will be checked.
	private BoxCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.

	private float inverseMoveTime;			//Used to make movement more efficient.

    protected Coroutine movementCoroutine;

    protected virtual void Start() {
		boxCollider = GetComponent<BoxCollider2D>();
		rb2D = GetComponent<Rigidbody2D>();

		inverseMoveTime = 1f / moveTime;
    }

    //Move returns true if it is able to move and false if not.
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit, bool move) {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if(hit.transform == null) {
            if(move) {
                movementCoroutine = StartCoroutine(SmoothMovement(end));
            }

            return true;
        }

        return false;
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end) {
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. //Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while(sqrRemainingDistance > float.Epsilon) {
			//Find a new position proportionally closer to the end, based on the moveTime.
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition(newPostion);
			
			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	//The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
	//AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
	protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component {
		RaycastHit2D hit;
		bool canMove = Move(xDir, yDir, out hit, false);

		if(hit.transform == null) {
			return;
		}

		T hitComponent = hit.transform.GetComponent<T>();

		if(!canMove && hitComponent != null) {
			OnCantMove(hitComponent);
		}
	}

	//The abstract modifier indicates that the thing being modified has a missing or incomplete implementation. OnCantMove will be overriden by functions in the inheriting classes.
	protected abstract void OnCantMove<T>(T component) where T : Component;
}