using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
	public class SaveService : IDisposable, IReadyFlag
	{
		public SaveDataState DataState { get; private set; } = new();
		public Config Config { get => _data.Config; set => SetConfig(value).Forget(); }

        public bool IsReady { get; private set; }

        public BehaviorSubject<Subject<SaverType>> SaveChoiceRequest { get; private set; } = new(null); 
		
		private IDataSaver _localDataSaver;
		private IDataSaver _cloudDataSaver;
		private CancellationTokenSource _cts = new();
		private SaveData _data;
        private bool _isDataLoaded;

        [Inject]
		private  async Task Construct(
		[Inject(Id = SaverType.Local)]IDataSaver localDataSaver, 
		[Inject(Id = SaverType.Cloud)]IDataSaver cloudDataSaver)
		{	
			Debug.Log("Start SaveService Initializing");
				
			_localDataSaver = localDataSaver;
			_cloudDataSaver = cloudDataSaver;

			var localData = await _localDataSaver.Load();
			var cloudData = await _cloudDataSaver.Load();
			
							
			if(cloudData == null)
            {
                _data = localData;
				Debug.Log($"Data was loaded from local");
            }
			else if (cloudData.DateSaving < localData.DateSaving)
			{	
				if(cloudData.DateSaving == default(DateTime))
                {
                    _data = localData;
                    Debug.Log($"Data was loaded from local");
                }
				else
                {
					var answerSubject = new Subject<SaverType>();				
					SaveChoiceRequest.OnNext(answerSubject);

                    IsReady = true;
					_data = new();
                    DataState.Initialize(_data);

                    Debug.Log("SaveService Initialized, Wait user choose");

                    SaverType answer = await answerSubject.FirstAsync(cancellationToken: _cts.Token);
					SaveChoiceRequest.OnCompleted();

					if (answer == SaverType.Cloud)
					{
						_data.Rewrite(cloudData);
                        Debug.Log($"Data was loaded from cloud");
					}	
					else
					{
                        _data.Rewrite(localData);
						Debug.Log($"Data was loaded from local");
					}

                    DataState.Initialize(_data);
                    _isDataLoaded = true;
                    return;
                }
			}
			else
            {
                _data = cloudData;
                Debug.Log($"Data was loaded from cloud");
            }

            DataState.Initialize(_data);

            IsReady = true;
			_isDataLoaded = true;
            SaveChoiceRequest.OnCompleted();
            Debug.Log("SaveService Initialized");
		}

		public void Dispose() 
		{
			_cts.Cancel();

			DataState.Dispose();

            if (IsReady && _isDataLoaded)
				ForceSave();
		}
		
		public void ForceSave()
        {
			_data.DateSaving = System.DateTime.Now;
			_localDataSaver.Save(_data);
			_cloudDataSaver.Save(_data);
        }

		private async UniTaskVoid SetConfig(Config config)
		{
			await UniTask.WaitWhile(() => !_isDataLoaded, cancellationToken: _cts.Token);

			Debug.Log("SetConfig");
			_data.Config = config;
        }
	}
}
