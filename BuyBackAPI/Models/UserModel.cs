using System;
using System.Runtime.Serialization;

namespace BuyBackAPI.Models
{
    [DataContract]
    public class UserModel
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "firstname")]
        public string Firstname { get; set; }

        [DataMember(Name = "middlename")]
        public string Middlename { get; set; }

        [DataMember(Name = "lastname")]
        public string Lastname { get; set; }

        [DataMember(Name = "dateofbirth")]
        public DateTime? DateOfBirth { get; set; }

        [DataMember(Name = "age")]
        public int? Age { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "mobileno")]
        public string Mobileno { get; set; }

        [DataMember(Name = "emailaddress")]
        public string Emailaddress { get; set; }

        [DataMember(Name = "bio")]
        public string Bio { get; set; }

        [DataMember(Name = "roleid")]
        public int? Roleid { get; set; }

        [DataMember(Name = "islocked")]
        public int? IsLocked { get; set; }

        [DataMember(Name = "profileimage")]
        public string ProfileImage { get; set; }
    }
}
