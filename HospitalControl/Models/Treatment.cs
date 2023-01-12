using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Курс лечения", DisplayNamePlural = "Курсы лечений", Description = "Выборка курсов лечения")]
public class Treatment
{
    public int Id { get; set; }
    public string Diagnosis { get; set; }
    public MedicalService MedicalService { get; set; }
    public Doctor Author { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public virtual ICollection<Drug> Drugs { get; set; } = new List<Drug>();
}