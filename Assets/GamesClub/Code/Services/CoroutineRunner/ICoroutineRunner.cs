using System.Collections;
using UnityEngine;

namespace GamesClub.Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
        void StopAllCoroutines();
    }
}