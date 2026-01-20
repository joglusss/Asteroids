using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

namespace Asteroids.Total
{
    public class CloudDataSaver: IDataSaver
	{
        bool _initialized;
        
        public async UniTask<SaveData> Load()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return null;

            if (_initialized == false)
            {   
                try
                {
                    await UnityServices.InitializeAsync().AsUniTask().Timeout(TimeSpan.FromSeconds(30));
                    await AuthenticationService.Instance.SignInAnonymouslyAsync().AsUniTask().Timeout(TimeSpan.FromSeconds(30));
                }
                catch  (TimeoutException)
                {
                    return null;
                }
                
                _initialized = true;
            }
            
            var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> {"Data"}).AsUniTask().Timeout(TimeSpan.FromSeconds(30));
            
            if (playerData.TryGetValue("Data", out var item)) 
            {
               return item.Value.GetAs<SaveData>();
            }
            
            return null;
        }
        
		public void Save(SaveData data)
        {
            Dictionary<string, object> save = new Dictionary<string, object> {{"Data", data}};   
            CloudSaveService.Instance.Data.Player.SaveAsync(save);
            
            Debug.Log("Save CloudData");
        }
    }

}

