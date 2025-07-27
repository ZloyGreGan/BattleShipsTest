using System;
using System.Collections.Generic;
using Game.Scripts.Entity.Components;
using UnityEngine;

namespace Game.Scripts.Entity
{
    public abstract class AEntity : MonoBehaviour, ITransformableComponent
    {
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public Vector3 Forward => transform.forward;
        
        private readonly List<IEntityComponent> _components = new();
        private readonly List<IUpdatableComponent> _updatableComponents = new();
        private readonly List<IStarterComponent> _starterComponents = new();
        
        private bool _isInitialized;
    
        public event Action<AEntity> OnEntityDestroyed;
    
        private void Awake()
        {
            if (_isInitialized) return;
            _isInitialized = true;
            OnInitialize();
            foreach (var component in _components)
            {
                component.Initialize();
            }
        }

        private void Start()
        {
            foreach (var component in _starterComponents)
            {
                component.Start();
            }
            OnStart();
        }

        private void Update()
        {
            foreach (var component in _updatableComponents)
            {
                component.Update();
            }
            OnUpdate();
        }

        protected abstract void OnInitialize();
        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
    
        #region Components Logic
    
        protected T AddComponent<T>() where T : IEntityComponent, new()
        {
            foreach (var component in _components)
            {
                if (component is T existingComponent)
                {
                    return existingComponent;
                }
            }

            T newComponent = new T();
            newComponent.SetOwner(this);
            _components.Add(newComponent);

            if (newComponent is IUpdatableComponent updatable)
            {
                _updatableComponents.Add(updatable);
                _updatableComponents.Sort((a, b) => 
                    ((a as IPrioritizedComponent)?.Priority ?? 0).CompareTo(
                        (b as IPrioritizedComponent)?.Priority ?? 0));
            }

            if (newComponent is IStarterComponent starter)
            {
                _starterComponents.Add(starter);
                _starterComponents.Sort((a, b) => 
                    ((a as IPrioritizedComponent)?.Priority ?? 0).CompareTo(
                        (b as IPrioritizedComponent)?.Priority ?? 0));
            }

            if (_isInitialized)
            {
                newComponent.Initialize();
            }

            return newComponent;
        }
    
        public T GetComponent<T>() where T : IEntityComponent
        {
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                {
                    return typedComponent;
                }
            }
            return default;
        }
    
        public bool HasComponent<T>() where T : IEntityComponent
        {
            return GetComponent<T>() != null;
        }
    
        public void RemoveComponent<T>() where T : IEntityComponent
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                if (_components[i] is T)
                {
                    var component = _components[i];
                    if (component is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    if (component is IUpdatableComponent updatable)
                    {
                        _updatableComponents.Remove(updatable);
                    }
                    if (component is IStarterComponent starter)
                    {
                        _starterComponents.Remove(starter);
                    }
                    _components.RemoveAt(i);
                    break;
                }
            }
        }
    
        #endregion
    
        private void OnDestroy()
        {
            foreach (var component in _components)
            {
                if (component is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _components.Clear();
            _updatableComponents.Clear();
            _starterComponents.Clear();
            OnEntityDestroyed?.Invoke(this);
        }
    }
}