using Asteroids.Total.Installers;
using Cysharp.Threading.Tasks;
using R3;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
	public class SaveService : IDisposable, IReadyFlag
	{
		public SaveData Data { get;  private set; }
		public Config Config { get => Data.Config; set => SetConfig(value).Forget(); }

        public bool IsReady { get; private set; }

        public BehaviorSubject<Subject<SaverType>> SaveChoiceRequest { get; private set; } = new(null); 
		
		private IDataSaver _localDataSaver;
		private IDataSaver _cloudDataSaver;
		private CancellationTokenSource _cts = new();
        public bool _isDataLoaded;

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
                Data = localData;
				Debug.Log($"Data was loaded from local");
            }
			else if (cloudData.DateSaving < localData.DateSaving)
			{	
				if(cloudData.DateSaving == default(DateTime))
                {
                    Data = localData;
                    Debug.Log($"Data was loaded from local");
                }
				else
                {
					var answerSubject = new Subject<SaverType>();				
					SaveChoiceRequest.OnNext(answerSubject);

                    IsReady = true;
					Data = new();

                    Debug.Log("SaveService Initialized, Wait user choose");

                    SaverType answer = await answerSubject.FirstAsync(cancellationToken: _cts.Token);
					SaveChoiceRequest.OnCompleted();

					if (answer == SaverType.Cloud)
					{
						Data.Rewrite(cloudData);
						Debug.Log($"Data was loaded from cloud");
					}	
					else
					{
                        Data.Rewrite(localData);
						Debug.Log($"Data was loaded from local");
					}

					_isDataLoaded = true;
                    return;
                }
			}
			else
            {
                Data = cloudData;
				Debug.Log($"Data was loaded from cloud");
            }
			
			IsReady = true;
			_isDataLoaded = true;
            SaveChoiceRequest.OnCompleted();
            Debug.Log("SaveService Initialized");
		}

		public void Dispose() 
		{
			_cts.Cancel();
		
			if(IsReady && _isDataLoaded)
				ForceSave();
		}
		
		public void ForceSave()
        {
			Data.DateSaving = System.DateTime.Now;
			_localDataSaver.Save(Data);
			_cloudDataSaver.Save(Data);
        }

		private async UniTaskVoid SetConfig(Config config)
		{
			await UniTask.WaitWhile(() => !_isDataLoaded, cancellationToken: _cts.Token);

			Debug.Log("SetConfig");
			Data.Config = config;
        }
	}
}
