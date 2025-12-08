using Asteroids.Total;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Menu
{ 
    public class SaveChooseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _panel;
        [SerializeField] private Button _cloudBtn;
        [SerializeField] private Button _localBtn;

        [Inject] private MenuViewModel _menuViewModel;

        private Subject<SaverType> _answer = new();

        private void Start()
        {
            Show(false);

            _cloudBtn.OnClickAsObservable()
                .Subscribe(_ => _answer.OnNext(SaverType.Cloud))
                .AddTo(this);
            _localBtn.OnClickAsObservable()
                .Subscribe(_ => _answer.OnNext(SaverType.Local))
                .AddTo(this);

            _menuViewModel.SaveTypeChoose.Subscribe(x => Show(x).Forget()).AddTo(this);
        }

        private async UniTaskVoid Show(Subject<SaverType> answer)
        {
            Debug.Log(answer == null);

            Show(true);

            answer.OnNext(await _answer.FirstAsync(cancellationToken: this.GetCancellationTokenOnDestroy()));

            Show(false);
        }

        private void Show(bool value)
        {
            _panel.alpha = value ? 1 : 0;
            _panel.interactable = value;
            _panel.blocksRaycasts = value;
        }
    }
}