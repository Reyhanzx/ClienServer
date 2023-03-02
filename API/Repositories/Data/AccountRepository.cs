using API.Contexts;
using API.Models;
using API.ViewModels;
using API.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class AccountRepository : GeneralRepository<int, Account>
{
    private readonly MyContext context;

    public AccountRepository(MyContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<int> Register(RegisterVM registerVM)
    {
        int result = 0;
        University university = new University
        {
            Name = registerVM.UniversityName
        };
        if (await context.Universities.AnyAsync(u => u.Name == university.Name))
        {
            university.Id = context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
        }
        else
        {
            await context.Universities.AddAsync(university);
            result = await context.SaveChangesAsync();
        }

        Education education = new Education
        {
            Major = registerVM.Major,
            Degree = registerVM.Degree,
            GPA = registerVM.GPA,
            UniversityId = university.Id
        };
        await context.Educations.AddAsync(education);
        await context.SaveChangesAsync();

        Employee employee = new Employee
        {
            Nik = registerVM.Nik,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            BirthDate = registerVM.BirthDate,
            Gender = (Models.GenderEnum)registerVM.Gender,
            HireingDate = registerVM.HireingDate,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
        };
        await context.Employees.AddAsync(employee);
        result= await context.SaveChangesAsync();

        Account account = new Account
        {
            EmployeeNik = registerVM.Nik,
            Password = Hashing.HashPassword(registerVM.Password)
        };
        await context.Accounts.AddAsync(account);
        result = await context.SaveChangesAsync();

        AccountRole accountRole = new AccountRole
        {
            AccountNik = registerVM.Nik,
            RoleId = 2
        };

        await context.AccountRoles.AddAsync(accountRole);
        result = await context.SaveChangesAsync();

        Profiling profiling = new Profiling
        {
            EmployeeId = registerVM.Nik,
            EducationId = education.Id
        };
        await context.Profilings.AddAsync(profiling);
        result = await context.SaveChangesAsync();
        return result;
    }

    public async Task< bool> Login(LoginVM loginVM)
    {
        var result = await context.Employees
            .Include(e => e.Account)
            .Select(e => new LoginVM
            {
                Email = e.Email,
                Password = e.Account.Password
            }).SingleOrDefaultAsync(a => a.Email == loginVM.Email);

        if (result is null)
        {
            return false;
        }
        return Hashing.ValidatePassword(loginVM.Password, result.Password);
    }

    public async Task<UserdataVM> GetUserdata(string key)
    {
        var userdata = (from e in context.Employees
                        join a in context.Accounts
                        on e.Nik equals a.EmployeeNik
                        join ar in context.AccountRoles
                        on a.EmployeeNik equals ar.AccountNik
                        join r in context.Roles
                        on ar.RoleId equals r.Id
                        where e.Email == key
                        select new UserdataVM
                        {
                            Email = e.Email,
                            FullName = String.Concat(e.FirstName, " ", e.LastName),

                        }).SingleOrDefaultAsync();

        return await userdata;
    }

    public async Task<IEnumerable<string>>  GetRolesByNik(string key)
    {
        var  getNik = context.Employees.FirstOrDefault(e => e.Email == key);
        return await context.AccountRoles.Where(ar => ar.AccountNik == getNik.Nik).Join(
            context.Roles,
            ar => ar.RoleId,
            r => r.Id,
            (ar, r) => r.Name).ToListAsync();
    }
}
