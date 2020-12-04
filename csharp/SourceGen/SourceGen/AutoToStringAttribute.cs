using System;

namespace HelloWorldSourceGen
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoToStringAttribute : Attribute
    {
    }
}