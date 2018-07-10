namespace ConsoleApp.Interface
{
    public interface IGenericClass<T> 
    {
        T Id { get; }
        string Name { get; }
        string Formula { get; }
        int Sides { get; }
        int Angles { get; }
        double Area { get; }
    }
}
