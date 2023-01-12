using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;


[MetaEntity(DisplayName = "Тип доктора", DisplayNamePlural = "Типы докторов", Description = "Выборка типов докторов")]
public class DoctorType
{
    public int Id { get; set; }

    public string Title { get; set; }
}