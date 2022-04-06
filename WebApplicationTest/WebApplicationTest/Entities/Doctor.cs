namespace WebApplicationTest.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public virtual Office Office { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual District District { get; set; }  
    }
}
