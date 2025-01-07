using RTS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IUICreate { void Create(); }

public interface IUICreate<in TParam> { void Create(TParam param); }

public interface IUICreate<in TParam1, in TParam2> { void Create(TParam2 param1, TParam1 param2); }

public class UIFactory : MonoBehaviour
{
    public class Factory : PlaceholderFactory<GameObject, GameObject>
    {
    }
}
