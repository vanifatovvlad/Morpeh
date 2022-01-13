#if UNITY_EDITOR
#define MORPEH_DEBUG
#endif
#if !MORPEH_DEBUG
#define MORPEH_DEBUG_DISABLED
#endif

namespace Morpeh {
    using System;
    using Collections;
    using Sirenix.OdinInspector;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SystemsGroup : IDisposable {
        [ShowInInspector]
        internal FastList<ISystem> systems;
        [ShowInInspector]
        internal FastList<ISystem> fixedSystems;
        [ShowInInspector]
        internal FastList<ISystem> lateSystems;

        [ShowInInspector]
        internal FastList<ISystem> disabledSystems;
        [ShowInInspector]
        internal FastList<ISystem> disabledFixedSystems;
        [ShowInInspector]
        internal FastList<ISystem> disabledLateSystems;

        [ShowInInspector]
        internal FastList<IInitializer> newInitializers;
        [ShowInInspector]
        internal FastList<IInitializer> initializers;
        [ShowInInspector]
        internal FastList<IDisposable> disposables;

        internal World  world;
        internal Action delayedAction;

        private SystemsGroup() {
        }

        internal SystemsGroup(World world) {
            this.world         = world;
            this.delayedAction = null;

            this.systems      = new FastList<ISystem>();
            this.fixedSystems = new FastList<ISystem>();
            this.lateSystems  = new FastList<ISystem>();

            this.disabledSystems      = new FastList<ISystem>();
            this.disabledFixedSystems = new FastList<ISystem>();
            this.disabledLateSystems  = new FastList<ISystem>();

            this.newInitializers = new FastList<IInitializer>();
            this.initializers    = new FastList<IInitializer>();
            this.disposables     = new FastList<IDisposable>();
        }

        public void Dispose() {
            if (this.disposables == null) {
                return;
            }

            void DisposeSystems(FastList<ISystem> systemsToDispose) {
                foreach (var system in systemsToDispose) {
#if MORPEH_DEBUG
                    try {
#endif
                        this.world.UpdateFilters();
                        system.Dispose();
#if MORPEH_DEBUG
                    }
                    catch (Exception e) {
                        Debug.LogError($"[MORPEH] Can not dispose system {system.GetType()}");
                        Debug.LogException(e);
                    }
#endif
                }

                systemsToDispose.Clear();
            }

            DisposeSystems(this.systems);
            this.systems = null;

            DisposeSystems(this.fixedSystems);
            this.fixedSystems = null;

            DisposeSystems(this.lateSystems);
            this.lateSystems = null;

            DisposeSystems(this.disabledSystems);
            this.disabledSystems = null;

            DisposeSystems(this.disabledFixedSystems);
            this.disabledFixedSystems = null;

            DisposeSystems(this.disabledLateSystems);
            this.disabledLateSystems = null;

            foreach (var initializer in this.newInitializers) {
#if MORPEH_DEBUG
                try {
#endif
                    this.world.UpdateFilters();
                    initializer.Dispose();
#if MORPEH_DEBUG
                }
                catch (Exception e) {
                    Debug.LogError($"[MORPEH] Can not dispose new initializer {initializer.GetType()}");
                    Debug.LogException(e);
                }
#endif
            }

            this.newInitializers.Clear();
            this.newInitializers = null;

            foreach (var initializer in this.initializers) {
#if MORPEH_DEBUG
                try {
#endif
                    this.world.UpdateFilters();
                    initializer.Dispose();
#if MORPEH_DEBUG
                }
                catch (Exception e) {
                    Debug.LogError($"[MORPEH] Can not dispose initializer {initializer.GetType()}");
                    Debug.LogException(e);
                }
#endif
            }

            this.initializers.Clear();
            this.initializers = null;

            foreach (var disposable in this.disposables) {
#if MORPEH_DEBUG
                try {
#endif
                    this.world.UpdateFilters();
                    disposable.Dispose();
#if MORPEH_DEBUG
                }
                catch (Exception e) {
                    Debug.LogError($"[MORPEH] Can not dispose system group disposable {disposable.GetType()}");
                    Debug.LogException(e);
                }
#endif
            }

            this.disposables.Clear();
            this.disposables = null;
        }
    }
}
