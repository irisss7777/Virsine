using System;
using Infrastructure.Factory.Box;
using Infrastructure.Factory.Loader;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private readonly LoaderFactory _loaderFactory;
        [Inject] private readonly BoxFactory _boxFactory;
        
        private void Awake()
        {
            _loaderFactory.CreateLoader();
            _boxFactory.CreateBox();
        }
    }
}