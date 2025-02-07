using Runtime.Interfaces;
using UnityEngine;

namespace Runtime
{
	[RequireComponent(typeof(ParticleSystem), typeof(AudioSource), typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class Brick : MonoBehaviour, IDamagable
	{
		
		[SerializeField] private int _health = 1;
		
		private ParticleSystem _particleSystem;
		private SpriteRenderer _spriteRenderer;
		private Collider2D _collider;
		private AudioSource _audioSource;

		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_particleSystem = GetComponent<ParticleSystem>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_collider = GetComponent<Collider2D>();

			var particleModule = _particleSystem.main;
			
			particleModule.startColor = _spriteRenderer.color;
		}
		
		public void Damage()
		{
			_health--;

			if (_health <= 0) HandleDeath();
		}

		private void HandleDeath()
		{
			_spriteRenderer.enabled = false;
			_collider.enabled = false;
			_audioSource.PlayOneShot(_audioSource.clip);
			_particleSystem.Play();
		}
	}
}