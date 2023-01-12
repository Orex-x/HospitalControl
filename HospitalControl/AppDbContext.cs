using HospitalControl.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalControl;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<DoctorType> DoctorTypes { get; set; }
    public DbSet<Drug> Drugs { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<Treatment> Treatments { get; set; }
    public DbSet<User> Users { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}