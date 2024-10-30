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

    /// <summary>
    /// 동기로 정보 가져오기
    /// </summary>
    public T LoadData<T>(string name) where T : class
    {
        var loadAsset = Addressables.LoadAssetAsync<T>(name);

        T t = loadAsset.WaitForCompletion();

        if (loadAsset.Status == AsyncOperationStatus.Succeeded)
        {
            return t;
        }

        throw new Exception($"Addressable Load Failed : {name}");
    }

    /// <summary>
    /// 비동기로 정보 가져오기
    /// </summary>
    public void LoadDataAsync<T>(string name, Action<T> callback) where T : class
    {
        var loadAsset = Addressables.LoadAssetAsync<T>(name);

        loadAsset.Completed += (handle) =>
        {
            callback?.Invoke(handle.Result); 
        };
    }
}
