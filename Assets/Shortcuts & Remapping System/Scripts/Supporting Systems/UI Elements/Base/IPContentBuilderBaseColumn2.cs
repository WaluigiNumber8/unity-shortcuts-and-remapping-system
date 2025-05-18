using UnityEngine;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// A base for all Property Builders working with a 2-column setup.
    /// </summary>
    public abstract class IPContentBuilderBaseColumn2<T> : IPContentBuilderBase<T>
    {
        protected readonly Transform contentSecond;
        
        protected IPContentBuilderBaseColumn2(Transform contentMain, Transform contentSecond) : base(contentMain)
        {
            this.contentSecond = contentSecond;
        }

        public override void Clear()
        {
            contentMain.ReleaseAllProperties();
            contentSecond.ReleaseAllProperties();
        }
    }
}