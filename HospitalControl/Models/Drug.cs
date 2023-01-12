using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Лекарство", DisplayNamePlural = "Лекарства", Description = "Выборка лекарств")]
public class Drug
{
    public int Id{ get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
}