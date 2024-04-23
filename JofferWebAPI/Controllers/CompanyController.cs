using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JofferWebAPI.Context;
using JofferWebAPI.Models;
using JofferWebAPI.Dtos;

namespace JofferWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DbContextRender _context;

        public CompanyController(DbContextRender context)
        {
            _context = context;
        }

        // GET: api/Company/5
        [HttpGet]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<CompanyDto>> GetCompany()
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(a => a.AccountId == account.Id);
            
            if (company == null)
            {
                return Problem("Not a company!");
            }

            return new CompanyDto(company);
        }

        // POST: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyDto companyDto)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'MyDbContext.Companies'  is null.");
            }

            AccountDto accountDto = new()
            {
                Name = companyDto.Name,
                Auth0Id = companyDto.Auth0Id,
                Email = companyDto.Email,
                IsPremium = companyDto.IsPremium,
                IsActive = true,
                AccountType = "Company",
                Password = ""
            };

            _context.Accounts.Add(new Account(accountDto));

            await _context.SaveChangesAsync();

            var recentlyCreatedAccount = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == accountDto.Auth0Id);

            if (recentlyCreatedAccount == null)
            {
                return Problem("Failed to retrieve recently created account.");
            }

            companyDto.AccountId = recentlyCreatedAccount.Id;
            
            var newCompany = new Company(companyDto);
            _context.Companies.Add(newCompany);
            await _context.SaveChangesAsync();

            companyDto.Id = newCompany.Id;

            return CreatedAtAction("GetCompany", new { id = companyDto.Id }, companyDto);
        }

        // PUT: api/Company
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [ServiceFilter(typeof(AuthActionFilter))]
        public async Task<ActionResult<CompanyDto>> PutCompany(CompanyDto companyDto)
        {
            if (HttpContext.Items["UserAccount"] is not Account account)
            {
                return Problem("Failed to fetch the account");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(t => t.AccountId == account.Id);

            if (company == null)
            {
                return Problem($"Company not found. (No company bindend to the account.)");
            }

            company.LogoUrl = companyDto.LogoUrl;
            company.Description = companyDto.Description;
            company.Image2Url = companyDto.Image2Url;
            company.Image3Url = companyDto.Image3Url;
            company.Image4Url = companyDto.Image4Url;
            company.Image5Url = companyDto.Image5Url;
            company.ComapnyUrl = companyDto.ComapnyUrl;
            company.LinkedInUrl = companyDto.LinkedInUrl;
            company.YoutubeUrl = companyDto.YoutubeUrl;
            company.InstaGramUrl = companyDto.InstaGramUrl;
            company.TwitterUrl = companyDto.TwitterUrl;
            company.IsActive = companyDto.IsActive;
            company.RecruiterToken = "000000";
            company.TokenActiveSince = (DateTime.Now).ToUniversalTime();

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (company == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            account.Auth0Id = companyDto.Auth0Id;
            account.Email = companyDto.Email;
            account.PhoneNumber = companyDto.PhoneNumber;
            account.Password = "NO PASSWORD";
            account.Name = companyDto.Name;
            account.AccountType = "Company";
            account.IsPremium = companyDto.IsPremium;
            account.IsActive = companyDto.IsActive;

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (account == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(t => t.Id == id);
            if (company == null) return Problem("Company does not exist.");


            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == company.AccountId);
            if (account == null) return Problem("Account connected to the company does not exist.");
            

            company.IsActive = false;
            account.IsActive = false;

            _context.Entry(company).State = EntityState.Modified;
            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }
            return await _context.Companies.ToListAsync();
        }
    }
}