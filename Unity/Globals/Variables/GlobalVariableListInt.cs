namespace Morpeh.Globals {
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using Unity.IL2CPP.CompilerServices;

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Globals/Variable List Int")]
    public class GlobalVariableListInt : BaseGlobalVariable<List<int>> {
        protected override List<int> Load(string serializedData)
            => JsonUtility.FromJson<ListIntWrapper>(serializedData).list;

        protected override string Save() => JsonUtility.ToJson(new ListIntWrapper {list = this.value});

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
        [Serializable]
        private struct ListIntWrapper {
            public List<int> list;
        }
    }
}