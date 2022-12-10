namespace Basics.ValueAndReferences
{
    internal class DogClass : IDog
    {
        public string Name { get; set; }
    }

    internal struct DogStruct : IDog
    {
        public string Name { get; set; }
    }

    internal interface IDog
    {
        string Name { get; set; }
    }
}
