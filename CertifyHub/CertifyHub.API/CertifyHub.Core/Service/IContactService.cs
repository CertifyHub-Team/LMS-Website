using CertifyHub.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertifyHub.Core.Service
{
    public interface IContactService
    {

        Task<List<Contact>> GetAllContact();
        void DeleteContact(int id);
        void CreateContact(Contact contact);
    }
}
