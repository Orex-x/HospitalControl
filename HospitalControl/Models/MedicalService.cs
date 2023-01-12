using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Услуга", DisplayNamePlural = "Услуги", Description = "Выборка услуг")]
public class MedicalService
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}