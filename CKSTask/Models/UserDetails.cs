using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CKSTask.Models
{
    public class UserDetails
    {
        public List<UserDetails> lstUserDetails { get; set; }
        public UserDetails objCaseDateils { get; set; }
        public Int64 UserDetailID { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }

        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public Int32 CreatedBy { get; set; }


        public string message { get; set; }
        public string code { get; set; }
        public Int64 UserAddressID { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Flag { get; set; }
    }
}
