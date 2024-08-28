namespace ModulyBack.Shared.Domain.Repositories;

public interface IUnitOfWork
{

    Task CompleteAsync();
}