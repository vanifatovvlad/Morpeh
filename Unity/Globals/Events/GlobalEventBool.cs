﻿namespace Morpeh.Globals {
    using System.Globalization;
    using Unity.IL2CPP.CompilerServices;
    using UnityEngine;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Globals/Events/Event Bool")]
    public class GlobalEventBool : BaseGlobalEvent<bool> {
        public override string Serialize(bool data) => data.ToString(CultureInfo.InvariantCulture);

        public override bool Deserialize(string serializedData) => bool.Parse(serializedData);
    }
}