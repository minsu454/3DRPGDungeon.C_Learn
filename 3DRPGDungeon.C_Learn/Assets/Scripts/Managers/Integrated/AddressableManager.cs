using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : IManager
{
    public void Init()
    {
        Addressables.InitializeAsync();
    }

    public T LoadItem<T>(string name) where T : class
    {
        var loadAsset = Addressables.LoadAssetAsync<T>(name);

        T t = loadAsset.WaitForCompletion();

        if (loadAsset.Status == AsyncOperationStatus.Succeeded)
        {
            return t;
        }

        throw new Exception($"Addressable Load Failed : {name}");
    }

    public void LoadItemAsync<T>(string name, Action<T> callback) where T : class
    {
        var loadAsset = Addressables.LoadAssetAsync<T>(name);

        loadAsset.Completed += (handle) =>
        {
            callback?.Invoke(handle.Result); 
        };
    }
}
