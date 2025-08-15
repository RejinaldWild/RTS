using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Score.Model;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;
using DeleteOptions = Unity.Services.CloudSave.Models.Data.Player.DeleteOptions;

namespace _RTSGameProject.Logic.Common.SaveLoad
{
    public class CloudSaveLoadService : ISaveService
    {
        private const string SCORE_GAME_DATA = "ScoreGameData";
        
        private readonly ISerializer _serializer;
        private ScoreGameData _scoreGameData;

        public CloudSaveLoadService(ISerializer serializer)
        {
            _serializer = serializer;
        }
        
        public async UniTask Initialize()
        {
            await SetupAndSign();
        }

        private async UniTask SetupAndSign()
        {
            try
            {
                await UnityServices.InitializeAsync();
                if (AuthenticationService.Instance.IsSignedIn == false &&
                    AuthenticationService.Instance.IsAuthorized == false)
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
            }
            catch (AuthenticationException ex)
            {
                Debug.LogWarning(ex);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public async UniTask<bool> IsSaveExist()
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                return false;
            }
            var allKeys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();
                foreach (var itemKey in allKeys)
                {
                    if (itemKey.Key.Equals(SCORE_GAME_DATA))
                    {
                        return true;
                    }
                }
            return false;
        }

        public async UniTask SaveAsync(ScoreGameData data)
        {
            string serializedData = await _serializer.ToJsonAsync(data);
            Dictionary<string, object> dictData = new Dictionary<string, object>{{SCORE_GAME_DATA, serializedData}};
            await CloudSaveService.Instance.Data.Player.SaveAsync(dictData);
        }

        public async UniTask<ScoreGameData> LoadAsync()
        {
            HashSet<string> keysToLoad = new HashSet<string> { SCORE_GAME_DATA };
            if (keysToLoad.Contains(SCORE_GAME_DATA))
            {
                Dictionary<string, Item> loadedData = await CloudSaveService.Instance.Data.Player.LoadAsync(keysToLoad);

                if (loadedData.TryGetValue(SCORE_GAME_DATA, out Item item))
                {
                    string serializedData = item.Value.GetAsString();
                    _scoreGameData = await _serializer.FromJsonAsync(serializedData);
                }
            }
            else
            {
                Debug.LogWarning("No data found!");
            }

            return _scoreGameData;
        }

        public async UniTask DeleteAsync()
        {
            DeleteOptions deleteOptions = new();
            await CloudSaveService.Instance.Data.Player.DeleteAsync(SCORE_GAME_DATA, deleteOptions);
        }

    }
}