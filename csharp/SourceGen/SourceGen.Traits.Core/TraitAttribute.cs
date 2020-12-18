using System;

namespace SourceGen.Traits.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TraitAttribute : Attribute
    {
        private readonly Type[] _types;

        public TraitAttribute(params Type[] types)
        {
            _types = types;
        }
    }
}