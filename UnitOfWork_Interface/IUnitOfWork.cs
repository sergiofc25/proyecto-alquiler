namespace UnitOfWork_Interface;

public interface IUnitOfWork
{
    IUnitOfWorkAdapter Create();
}