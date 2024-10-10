using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    private Dictionary<Type, IPlayerComponent> _componets;
    public PlayerMovement MovementCompo { get; private set; }

    private void Awake()
    {
        _componets = new Dictionary<Type, IPlayerComponent>();

        GetComponentsInChildren<IPlayerComponent>().ToList()
            .ForEach(x=>_componets.Add(x.GetType(),x));
        _componets.Add(_inputReader.GetType(), _inputReader);
        _componets.Values.ToList().ForEach(compo => compo.Initialize(this));
        MovementCompo = GetComponent<PlayerMovement>();
    }
    
    public T GetCompo<T>()where T : class
    {
        Type type = typeof(T);
        if(_componets.TryGetValue(type,out IPlayerComponent compo))
        {
            return compo as T;
        }
        return default;
    }
}
