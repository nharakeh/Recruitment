using Microsoft.AspNetCore.Mvc;
using Vuture.Exceptions.ExceptionResponses;
using Vuture.Models.Dtos;
using Vuture.Persistence;
using Vuture.Services;

namespace Vuture.Controllers
{
   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactDbContext _context;
        private ContactService _service;

         public ContactController(ContactDbContext context)
        {
            _context = context;
            _service = new ContactService(_context);
          

        }
       

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetContactById(int id)
        {
         
            ReadContactDto contact = _service.GetContactById(id);
        
            if (contact == null) // if contact not found
            {
                return NotFound();
            }
            return Ok(contact);


           // throw new NotImplementedException();
        }

        [HttpGet("GetContactByCompany/{company}")]
  
        public IActionResult GetContactByCompany(string company)
        {
            
            List<ReadContactDto> contacts = _service.GetContactByCompany(company);

            if (contacts == null)
            {
                return NotFound();
            }
            return Ok(contacts);


            // throw new NotImplementedException();
        }
       
        [HttpPost]
        [Route("")]
        public ActionResult<ReadContactDto> CreateContact([FromBody] CreateContactDto createContactDto)
        {
            
            var existing = _context.Contacts.Where(x=> x.EmailAddress == createContactDto.EmailAddress).FirstOrDefault(); //checking if another contact has the same email address
            if(existing != null)
                return Conflict();
            var newcontact = new Contact();

            newcontact.Id = 0;
            newcontact.FirstName = createContactDto.FirstName;
            newcontact.LastName = createContactDto.LastName;
            newcontact.EmailAddress = createContactDto.EmailAddress;
            newcontact.Title = createContactDto.Title;
            newcontact.Company = createContactDto.Company;
            newcontact.Status = createContactDto.Status;
            if (!TryValidateModel(newcontact))
            {
                throw new BadRequestExceptionResponse("Invalid model");

            }


            ReadContactDto contact = _service.CreateContact(createContactDto);
            var c = _context.Contacts.Select(b =>
             new ReadContactDto()
             {
                 Id = b.Id,
                 FirstName = b.FirstName,
                 LastName = b.LastName,
                 EmailAddress = b.EmailAddress,
                 Title = b.Title,
                 Company = b.Company,
                 Status = b.Status
             }).ToList();

         
           
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(c);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ReadContactDto> UpdateContactById(int id, UpdateContactDto updateContactDto)
        {
            
            var existing = _context.Contacts.Where(x => x.EmailAddress == updateContactDto.EmailAddress && x.Id != id).FirstOrDefault(); // making sure no other contact has the same email
            if (existing != null)
                return Conflict();
            ReadContactDto contact = _service.UpdateContactById(id , updateContactDto);
            var c = _context.Contacts.Select(b =>
             new ReadContactDto()
             {
                 Id = b.Id,
                 FirstName = b.FirstName,
                 LastName = b.LastName,
                 EmailAddress = b.EmailAddress,
                 Title = b.Title,
                 Company = b.Company,
                 Status = b.Status
             }).ToList();

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(c);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteContactById(int id)
        {

            _service.DeleteContactById(id);
            var c = _context.Contacts.Select(b =>
            new ReadContactDto()
            {
                Id = b.Id,
                FirstName = b.FirstName,
                LastName = b.LastName,
                EmailAddress = b.EmailAddress,
                Title = b.Title,
                Company = b.Company,
                Status = b.Status
            }).ToList();

            
            return Ok(c);
        }
    }
}