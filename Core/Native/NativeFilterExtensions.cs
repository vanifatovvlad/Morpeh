﻿#if MORPEH_BURST
namespace Morpeh.Native {
    using System.Runtime.CompilerServices;
    using Native;
    using Unity.Collections;

    public static class NativeFilterExtensions {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NativeFilter AsNative(this Filter filter) => new NativeFilter(filter.AsNativeWrapper(), filter.GetLengthSlow());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NativeFilter AsNative(this Filter filter, int length) => new NativeFilter(filter.AsNativeWrapper(), length);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static NativeFilterWrapper AsNativeWrapper(this Filter filter) {
            // TODO: Get rid of archetypes NativeArray allocation (?)
            var nativeFilter = new NativeFilterWrapper {
                archetypes = new NativeArray<NativeArchetype>(filter.archetypes.length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory),
                world = filter.world.AsNative(),
            };

            for (int i = 0, length = filter.archetypes.length; i < length; i++) {
                nativeFilter.archetypes[i] = filter.archetypes.data[i].AsNative();
            }

            return nativeFilter;
        }
    }
}
#endif