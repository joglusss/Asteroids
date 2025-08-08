using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Menu
{
	public class MenuView : MonoBehaviour
	{
		[SerializeField] private Button _startGame;
		[SerializeField] private Button _exit;
		[SerializeField] private TMP_Text _bestScore;
		[SerializeField] private TMP_Text _lastScore;
		
		private MenuViewModel _viewModel;
		
		[Inject]
		private void Construct(MenuViewModel menuViewModel)
		{
			_viewModel = menuViewModel;
		}

		private void Start()
		{
			_startGame.OnClickAsObservable()
				.Subscribe(e => GoToGame())
				.AddTo(this);
			_exit.OnClickAsObservable()
				.Subscribe(e => Exit())
				.AddTo(this);
			_viewModel.BestScore
				.Subscribe(UpdateBestScore)
				.AddTo(this);
			_viewModel.LastScore
				.Subscribe(UpdateLastScore)
				.AddTo(this);
		}

		private void OnDestroy()
		{
			_startGame.onClick.RemoveListener(GoToGame);
			_exit.onClick.RemoveListener(Exit);
		}

		private void GoToGame() => _viewModel.StartGame();

		private void Exit() => _viewModel.ExitGame();

		private void UpdateBestScore(string value) => _bestScore.text = value;

		private void UpdateLastScore(string value) => _lastScore.text = value;
	}
}