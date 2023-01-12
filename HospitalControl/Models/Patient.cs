using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Пациент", DisplayNamePlural = "Пациенты", Description = "Выборка пациентов")]
public class Patient
{
    public int Id { get; set; }
            
    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Policy { get; set; }
    public string Registration { get; set; }

    public User User { get; set; }

    public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();
    public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();
}