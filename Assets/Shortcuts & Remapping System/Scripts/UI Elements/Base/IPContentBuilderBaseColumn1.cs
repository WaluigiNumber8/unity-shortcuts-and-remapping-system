using UnityEngine;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a single column.
    /// </summary>
    public abstract class IPContentBuilderBaseColumn1<T> : IPContentBuilderBase<T>
    {
        protected IPContentBuilderBaseColumn1(Transform contentMain) : base(contentMain) { }

        public override void Clear()
        {
            contentMain.ReleaseAllProperties();
        }
    }
}