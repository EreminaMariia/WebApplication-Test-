using WebApplicationTest.Entities;

namespace WebApplicationTest.Views
{
    public class PatientView
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<int> Districts { get; set; }

        public PatientView (Patient patient)
        {
            Id = patient.Id;
            LastName = patient.LastName;    
            Name = patient.Name;
            Patronymic = patient.Patronymic;
            Address = patient.Address;
            DateOfBirth = patient.DateOfBirth;

            Districts = new List<int>();
            foreach (var district in patient.Districts)
            {
                Districts.Add(district.Number);
            }
        }

        public static List<PatientView> MapList(List<Patient> patients)
        {
            List<PatientView> patientViews = new List<PatientView>();
            foreach (var patient in patients)
            {
                patientViews.Add(new PatientView(patient));
            }

            return patientViews;
        }
    }
}
