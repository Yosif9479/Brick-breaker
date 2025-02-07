using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
	public class GameOverPanel : MonoBehaviour
	{
		[SerializeField] private GameObject _menu;

		private void Awake()
		{
			_menu.SetActive(false);
			Time.timeScale = 1;
		}

		private void OnEnable()
		{
			GameEventBus.GameOver += OnGameOver;
		}

		private void OnDisable()
		{
			GameEventBus.GameOver -= OnGameOver;
		}

		public void Restart()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		private void OnGameOver()
		{
			Time.timeScale = 0;
			
			_menu.SetActive(true);
		}
	}
}