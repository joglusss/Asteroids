using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuView : MonoBehaviour, IInitializable, IDisposable
	{
		[SerializeField] private Button _startGame;
		[SerializeField] private Button _exit;
		[SerializeField] private TMP_Text _bestScore;
		[SerializeField] private TMP_Text _lastScore;
		
		private MenuViewModel _viewModel;
		private CompositeDisposable _disposables = new CompositeDisposable();
		
		[Inject]
		private void Construct(MenuViewModel menuViewModel)
		{
			_viewModel = menuViewModel;
		}

		public void Initialize()
		{
			_startGame.onClick.AddListener(GoToGame);
			_exit.onClick.AddListener(Exit);

			_viewModel.BestScore.Subscribe(UpdateBestScore).AddTo(_disposables);
			_viewModel.LastScore.Subscribe(UpdateLastScore).AddTo(_disposables);
		}

		public void Dispose()
		{
			_startGame.onClick.RemoveListener(GoToGame);
			_exit.onClick.RemoveListener(Exit);
			_disposables.Dispose();
		}

		private void GoToGame() => _viewModel.StartGame();

		private void Exit() => _viewModel.ExitGame();

		private void UpdateBestScore(int value) => _bestScore.text = $"BEST SCORE: {value}";

		private void UpdateLastScore(int value) => _lastScore.text = $"Last score: {value}";
	}
}