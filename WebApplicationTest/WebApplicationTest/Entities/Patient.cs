namespace WebApplicationTest.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
