using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
	public static class GameEventBus
	{
		public static event UnityAction GameOver;
		
		public static void TriggerGameOver()
		{
			GameOver?.Invoke();
		}
	}
}