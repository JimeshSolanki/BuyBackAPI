using System;
using System.Runtime.Serialization;

namespace BuyBackAPI.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "Username")]
        public string Username { get; set; }

        [DataMember(Name = "Password")]
        public string Password { get; set; }

        [DataMember(Name = "Firstname")]
        public string Firstname { get; set; }

        [DataMember(Name = "Middlename")]
        public string Middlename { get; set; }

        [DataMember(Name = "Lastname")]
        public string Lastname { get; set; }

        [DataMember(Name = "Dateofbirth")]
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "Age")]
        public int? Age { get; set; }

        [DataMember(Name = "Gender")]
        public string Gender { get; set; }

        [DataMember(Name = "Mobileno")]
        public string Mobileno { get; set; }

        [DataMember(Name = "Emailaddress")]
        public string Emailaddress { get; set; }

        [DataMember(Name = "Bio")]
        public string Bio { get; set; }

        [DataMember(Name = "Roleid")]
        public int? Roleid { get; set; }

        [DataMember(Name = "Rolename")]
        public string Rolename { get; set; }

        [DataMember(Name = "Islocked")]
        public int? IsLocked { get; set; }

        [DataMember(Name = "Profileimage")]
        public string ProfileImage { get; set; }
    }
}
