using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class ContactDB
    {
        private MudPieEntities db;
        public ContactDB()
        {
            db = new MudPieEntities();
        }
        //to get contact by Id
       public Contact GetByID(int contactId)
        {
            //return db.Contacts.Find(contactId);
          return  db.Contacts.Where(x => x.ContactID == contactId).FirstOrDefault();
        }
        //insert table not class
        public void Insert(Contact contact)
        {
            db.Contacts.Add(contact);
            save();
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
