using R3;
using System;
using System.IO;
using UnityEngine;
using Zenject;

namespace Asteroids.Total
{
	public class SaveManager : IInitializable, IDisposable
	{
		public readonly ReactiveProperty<SaveData> Data = new();
		
		private IDataSaver _dataSaver;

		[Inject]
		private void Construct(IDataSaver dataSaver)
		{ 
			_dataSaver = dataSaver;
		}

		public void Initialize()
		{
			Data.Value = _dataSaver.Load();
		}

		public void Dispose() 
		{
			_dataSaver.Save(Data.Value);
		}
	}
}
