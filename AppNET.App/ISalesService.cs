using System.Transactions;

namespace AppNET.App
{
    public interface ISalesService
    {
        void Create(int id, Transaction gelir, Transaction gider);
        bool Delete(int id);
        IReadOnlyCollection<Vault> GetAll();
        Vault Update(int id, Transaction gelir, Transaction gider);
        Vault GetById(int id);
    }
}