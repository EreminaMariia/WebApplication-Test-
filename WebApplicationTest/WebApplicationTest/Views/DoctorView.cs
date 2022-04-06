using WebApplicationTest.Entities;

namespace WebApplicationTest.Views
{
    public class DoctorView
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Office { get; set; }
        public string Specialization { get; set; }
        public int District { get; set; }

        public DoctorView (Doctor doctor)
        {
            Id = doctor.Id;
            FullName = doctor.FullName;
            Office = doctor.Office.Number;
            Specialization = doctor.Specialization.Name;
            District = doctor.District.Number;
        }

        public static List<DoctorView> MapList (List<Doctor>doctors)
        {
            List<DoctorView> doctorViews = new List<DoctorView> ();
            foreach (var doctor in doctors)
            {
                doctorViews.Add (new DoctorView (doctor));
            }

            return doctorViews;
        }
    }
}
