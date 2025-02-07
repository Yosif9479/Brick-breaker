using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
	[RequireComponent(typeof(EdgeCollider2D))]
	public class ScreenCollider : MonoBehaviour
	{
		private Camera _camera;
		private EdgeCollider2D _edge;
        
		private void Awake()
		{
			_edge = GetComponent<EdgeCollider2D>();
			_camera = Camera.main;
			LimitField();   
		}

		private void LimitField()
		{
			var leftBottom = _camera.ScreenToWorldPoint(new Vector2(0, 0));
			var rightBottom = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
			var leftTop = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height));
			var rightTop = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

			var points = new List<Vector2> { leftBottom, rightBottom, rightTop, leftTop, leftBottom };
            
			_edge.SetPoints(points);
		}
	}
}