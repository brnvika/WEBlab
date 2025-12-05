using MediatR;
using TRKApp.Models;

namespace TRKApp.Models.Commands
{
    public class CreateContact : IRequest<Contact>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Question { get; set; }
    }

    public class CreateContactHandler : IRequestHandler<CreateContact, Contact>
    {
        private readonly DataContext _context;

        public CreateContactHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Contact> Handle(CreateContact request, CancellationToken cancellationToken)
        {
            var contact = new Contact
            {
                Name = request.Name,
                Email = request.Email,
                Question = request.Question,
                CreatedAt = DateTime.UtcNow
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return contact;
        }
    }
}