using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Запись на приём", DisplayNamePlural = "Записи на приём", Description = "Выборка записей на приём")]
public class Record
{
    public int Id { get; set; }
    public RecordStatus Status { get; set; }
    public DateTime DateTime { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}