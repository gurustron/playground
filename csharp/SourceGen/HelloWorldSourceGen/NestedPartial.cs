using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HelloWorldSourceGen
{
    public partial class Nested
    {
        static void M()
        {
            Expression<Func<Parent, bool>> parentExpression = p => true;
            Expression<Func<Child, bool>> childExpression = c => true;
            Action x = null;
            x?.Invoke();
            // Expression<Func<Parent, ICollection<Child>>> access = x => x.Children; 
            var parameter = Expression.Parameter(typeof(Parent));
            var property = Expression.Property(parameter, nameof(Parent.Children));
            
            
            
        }
    }
    
    public class Parent
    {
        public int Id { get; set; }
        public ICollection<Child> Children{ get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public Parent Parent { get; set; }
        public int ParentId { get; set; }
    }

}