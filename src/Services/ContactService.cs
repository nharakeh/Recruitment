using Vuture.Models.Dtos;
using Vuture.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Vuture.Services
{
    public class ContactService : IContactService
    {
        private ContactDbContext _context;
        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public ReadContactDto CreateContact(CreateContactDto dto)
        {


            //Determine the next ID
            int newID = _context.Contacts.Select(x => x.Id).Max() + 1;

            var newcontact = new Contact();

            newcontact.Id = newID;
            newcontact.FirstName = dto.FirstName;
            newcontact.LastName = dto.LastName;
            newcontact.EmailAddress = dto.EmailAddress;
            newcontact.Title = dto.Title;
            newcontact.Company = dto.Company;
            newcontact.Status = dto.Status;


            _context.Contacts.Add(newcontact); // add the contact to the table
            _context.SaveChanges();
            var dtoread = new ReadContactDto()
            {
                Id = newcontact.Id,
                FirstName = newcontact.FirstName,
                LastName = newcontact.LastName,
                EmailAddress = newcontact.EmailAddress,
                Title = newcontact.Title,
                Company = newcontact.Company,
                Status = newcontact.Status
            };
            return dtoread;

        }

        public ReadContactDto UpdateContactById(int id, UpdateContactDto dto)
        {
            var contact = _context.Contacts.Where(x => x.Id == id).FirstOrDefault();
            if (contact != null)
            {
                contact.Status = dto.Status;
                contact.LastName = dto.LastName;
                contact.FirstName = dto.FirstName;
                contact.EmailAddress = dto.EmailAddress.ToString();
                contact.Title = dto.Title;
                contact.Company = dto.Company;
                _context.SaveChanges();
            }
            return GetContactById(id);

        }

        public ReadContactDto GetContactById(int id)
        {

            var contact = _context.Contacts.Select(b =>
                      new ReadContactDto()
                      {
                          Id = b.Id,
                          FirstName = b.FirstName,
                          LastName = b.LastName,
                          EmailAddress = b.EmailAddress,
                          Title = b.Title == null ? "" : b.Title,
                          Company = b.Company == null ? "" : b.Company,
                          Status = b.Status == null ? "" : b.Status,
                      }).FirstOrDefault(b => b.Id == id);

            return contact;

        }

        public List<ReadContactDto> GetContactByCompany(string company)
        {

            var contacts = (from b in _context.Contacts.AsEnumerable()
                            where (b.Company.ToLower() == company.ToLower())  // the checking will be not case sensitive
                            select new ReadContactDto()
                            {
                                Id = b.Id,
                                FirstName = b.FirstName,
                                LastName = b.LastName,
                                EmailAddress = b.EmailAddress,
                                Title = b.Title == null ? "" : b.Title,
                                Company = b.Company == null ? "" : b.Company,
                                Status = b.Status == null ? "" : b.Status,
                            }).ToList();

            return contacts;

        }
        public void DeleteContactById(int id)

        {

            var contact = _context.Contacts.Where(x => x.Id == id).FirstOrDefault();
            if (contact != null)
            {
                _context.Remove(contact);
                _context.SaveChanges();
            }

        }
    }
}
