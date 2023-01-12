using EasyData.EntityFrameworkCore;

namespace HospitalControl.Models;

[MetaEntity(DisplayName = "Пользователь", DisplayNamePlural = "Пользователи", Description = "Выборка пользователей")]
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}