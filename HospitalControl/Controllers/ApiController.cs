using HospitalControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalControl.Controllers;

[Route("api/easydata/models/__default/sources")]
public class ApiController : Controller
{
    private AppDbContext _db;
    public ApiController(AppDbContext db)
    {
        _db = db;
    }
    
    #region GET
    [Route("Doctor/get")]
    public async Task<IEnumerable<Doctor>> Doctors()
        => await _db.Doctors
            .Include(x => x.User)
            .Include(x => x.DoctorType)
            .ToArrayAsync();
    
    [Route("DoctorType/get")]
    public async Task<IEnumerable<DoctorType>> DoctorTypes()
        => await _db.DoctorTypes
            .ToArrayAsync();
    
    [Route("Drug/get")]
    public async Task<IEnumerable<Drug>> Drugs()
        => await _db.Drugs
            .ToArrayAsync();
    
    [Route("MedicalService/get")]
    public async Task<IEnumerable<MedicalService>> MedicalServices()
        => await _db.MedicalServices
            .ToArrayAsync();
    
    [Route("Patient/get")]
    public async Task<IEnumerable<Patient>> Patients()
        => await _db.Patients
            .Include(x => x.User)
            .Include(x => x.MedicalServices)
            .Include(x => x.Drugs)
            .ToArrayAsync();
    
    [Route("Record/get")]
    public async Task<IEnumerable<Record>> Records()
        => await _db.Records
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .ToArrayAsync();
    
    [Route("Treatment/get")]
    public async Task<IEnumerable<Treatment>> Treatments()
        => await _db.Treatments
            .Include(x => x.MedicalService)
            .Include(x => x.Doctor)
            .Include(x => x.Author)
            .Include(x => x.Patient)
            .Include(x => x.Drugs)
            .ToArrayAsync();
    
    [Route("User/get")]
    public async Task<IEnumerable<User>> Users()
        => await _db.Users
            .ToArrayAsync();
    
    #endregion
}