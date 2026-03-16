namespace UnitOfWork_Interface;

public interface IUnitOfWorkAdapter : IDisposable
{
    IUnitOfWorkRepository Repositories { get; }

    void SaveChanges();
}