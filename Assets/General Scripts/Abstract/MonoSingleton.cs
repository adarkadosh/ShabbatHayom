using System;
using UnityEngine;

/// <summary>
/// A generic Singleton class for MonoBehaviours.
/// Example usage: public class GameManager : MonoSingleton<GameManager>
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    protected virtual void Awake()
    {
        
    }

    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = FindAnyObjectByType<T>();
            if (_instance == null)
            {
                var singletonObject = new GameObject(typeof(T).Name);
                _instance = singletonObject.AddComponent<T>();
                DontDestroyOnLoad(singletonObject);
            }

            return _instance;
        }
    }

    // Ensure no other instances can be created by having the constructor as protected
    protected MonoSingleton()
    {
    }
}