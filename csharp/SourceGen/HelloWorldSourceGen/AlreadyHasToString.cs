namespace HelloWorldSourceGen
{
    public partial class AlreadyHasToString
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}