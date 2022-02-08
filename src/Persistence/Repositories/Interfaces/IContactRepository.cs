namespace Vuture.Persistence.Repositories.Interfaces
{
    public interface IContactRepository
    {
        void SaveChanges();
        Contact GetContactById(int Id);
        List<Contact> GetContactByCompany(string Company);
        
        void DeleteContactById(int Id);
        Contact CreateContact(Contact Contact);
        Contact UpdateContact(Contact Contact);
    }
}