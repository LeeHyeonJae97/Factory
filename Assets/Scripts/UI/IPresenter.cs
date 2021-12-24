using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPresenter<T> where T : class
{
    void SetInfo(T t);
}
