using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;


[MetaEntity(DisplayName = "Доктор", DisplayNamePlural = "Доктора", Description = "Выборка докторов")]
public class Doctor
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public int Experience { get; set; }
    public DoctorType DoctorType { get; set; }
    public User User { get; set; }
}