using R3;
using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
	public class SaveService : IInitializable, IDisposable
	{
		public SaveData Data { get;  private set; }
		
		private IDataSaver _dataSaver;

		[Inject]
		private void Construct(IDataSaver dataSaver)
		{ 
			_dataSaver = dataSaver;
		}

		public void Initialize()
		{
			Data = _dataSaver.Load();
		}

		public void Dispose() 
		{
			_dataSaver.Save(Data);
		}
	}
}
