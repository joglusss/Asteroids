using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Ship
{
	public class ShipStatView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _healthText;
		[SerializeField] private TMP_Text _coordinatesText;
		[SerializeField] private TMP_Text _angleText;
		[SerializeField] private TMP_Text _speedText;
		[SerializeField] private TMP_Text _laserCountText;
		[SerializeField] private TMP_Text _laserCooldownText;
		[SerializeField] private Button _adsBtn;
		[SerializeField] private Button _giveUpBtn;

		private ShipStatViewModel _viewModel;

		[Inject]
		private void Construct(ShipStatViewModel viewModel)
		{ 
			_viewModel = viewModel;
		}

		public void Start()
		{
			_viewModel.Health.Subscribe(ChangeHealth).AddTo(this);
			_viewModel.Coordinates.Subscribe(ChangeCoordinates).AddTo(this);
			_viewModel.Angle.Subscribe(ChangeAngle).AddTo(this);
			_viewModel.Speed.Subscribe(ChangeSpeed).AddTo(this);
			_viewModel.LaserCount.Subscribe(ChangeLaserCount).AddTo(this);
			_viewModel.LaserCooldown.Subscribe(ChangeLaserCooldown).AddTo(this);

			_viewModel.LifeStatus.Subscribe(ShowButton).AddTo(this);
			
			_adsBtn.OnClickAsObservable().Subscribe(_ => _viewModel.Resurrect()).AddTo(this);
			_giveUpBtn.OnClickAsObservable().Subscribe(_ => _viewModel.GiveUp()).AddTo(this);
		}

		public void ChangeHealth(string value) => _healthText.text = value;

		public void ChangeCoordinates(string value) => _coordinatesText.text = value;

		public void ChangeAngle(string value) => _angleText.text = value;

		public void ChangeSpeed(string value) => _speedText.text = value;

		public void ChangeLaserCount(string value) => _laserCountText.text = value;

		public void ChangeLaserCooldown(string value) => _laserCooldownText.text = value;
		
		public void ShowButton(bool value)
		{
			_adsBtn.gameObject.SetActive(!value);
			_giveUpBtn.gameObject.SetActive(!value);
		}
	}

}
