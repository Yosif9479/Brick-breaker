using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
	[RequireComponent(typeof(EdgeCollider2D))]
	public class Deadzone : MonoBehaviour
	{
		private EdgeCollider2D _collider;
		
		private void Awake()
		{
			_collider = GetComponent<EdgeCollider2D>();
			Camera camera = Camera.main!;
			
			var left = camera.ScreenToWorldPoint(new Vector2(0, 0));
			var right = camera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

			_collider.SetPoints(new List<Vector2> { left, right });
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.GetComponent<Ball>())
			{
				GameEventBus.TriggerGameOver();
			}
		}
		
		private void OnValidate()
		{
			GetComponent<EdgeCollider2D>().isTrigger = true;
		}
	}
}