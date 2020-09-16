using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContactBs
    {
        private ContactDB objDb;
        public ContactBs()
        {
            objDb = new ContactDB();
        }
        public Contact GetByID(int contactId)
        {
            return objDb.GetByID(contactId);
        }
        public void Insert(Contact contact)
        {
            objDb.Insert(contact);
        }
      }

}
