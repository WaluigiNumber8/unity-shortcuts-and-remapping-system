using UnityEngine;

namespace RedRats.UI.Properties
{
    /// <summary>
    /// A base for all Property Builders.
    /// </summary>
    public abstract class IPContentBuilderBase<T> : IIPContentBuilder
    {
        protected readonly UIPropertyBuilder b;
        protected readonly Transform contentMain;

        protected IPContentBuilderBase(Transform contentMain)
        {
            b = UIPropertyBuilder.Instance;
            this.contentMain = contentMain;
        }

        /// <summary>
        /// Build properties for the asset.
        /// </summary>
        public void Build(T asset)
        {
            Clear();
            BuildInternal(asset);
        }
        
        public abstract void BuildInternal(T asset);
        
        /// <summary>
        /// Empty contents.
        /// </summary>
        public abstract void Clear();
    }
}