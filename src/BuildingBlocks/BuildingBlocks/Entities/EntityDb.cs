namespace BuildingBlocks.Entities;

public class EntityDb<T> where T : struct
{
    public T Id { get; set; }
}
