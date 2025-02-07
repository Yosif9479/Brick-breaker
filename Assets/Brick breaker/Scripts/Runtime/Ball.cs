using Runtime.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime
{
	[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
	public class Ball : MonoBehaviour
	{
		public Vector2 Direction;
		
		[SerializeField] private float _movementSpeed = 1;
		
		[Range(0, 90)]
		[SerializeField] private float _initialDirectionRange = 45f;
		
		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			SetRandomDirection();
		}

		private void FixedUpdate()
		{
			ApplyMovement();
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			GameObject hitObject = collision.gameObject;
			
			_audioSource.Play();
			
			if (hitObject.GetComponent<Player>()) return;

			if (hitObject.GetComponent<IDamagable>() is IDamagable damagable)
			{
				damagable.Damage();
			}
			
			Direction = Vector2.Reflect(Direction, collision.contacts[0].normal);
		}

		private void ApplyMovement()
		{
			transform.Translate(Direction * (_movementSpeed * Time.fixedDeltaTime));
		}
		
		private void SetRandomDirection()
		{
			float angle = 90 - Random.Range(0, _initialDirectionRange);
			bool invert = Random.value > 0.5f;
			float radians = angle * Mathf.Deg2Rad;

			float sin = Mathf.Sin(radians);
			float cos = Mathf.Cos(radians);
			cos = invert ? -cos : cos;
			
			Direction = new Vector2(cos, sin);
		}
	}
}