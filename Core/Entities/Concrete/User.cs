using System;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public bool PaswordChange { get; set; }
        public short Fk_Depo { get; set; }
        public int Fk_User { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
