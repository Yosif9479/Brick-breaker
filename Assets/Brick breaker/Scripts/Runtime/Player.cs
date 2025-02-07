using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime
{
	[RequireComponent(typeof(Collider2D))]
	public class Player : MonoBehaviour
	{
		[SerializeField] private float _movementSpeed = 1f;
		
		private InputAction _moveAction;
		private Collider2D _collider;
		
		private void Awake()
		{
			_moveAction = InputSystem.actions.FindAction("Move");
			_collider = GetComponent<Collider2D>();
		}

		private void FixedUpdate()
		{
			ApplyMovement();
		}

		private void ApplyMovement()
		{
			var direction = _moveAction.ReadValue<Vector2>();

			direction.y = 0;
			
			transform.Translate(direction * (_movementSpeed * Time.fixedDeltaTime));
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.GetComponent<Ball>() is not Ball ball) return;

			var contact = collision.GetContact(0);

			float offset = (contact.point.x - transform.position.x) / (_collider.bounds.size.x / 2);
			offset = Mathf.Clamp(offset, -0.8f, 0.8f);
			float angle = Mathf.Acos(offset);

			Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			
			ball.Direction = direction.normalized;
		}

	}
}