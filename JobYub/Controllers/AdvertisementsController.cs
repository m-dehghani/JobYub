using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobYub.Data;
using JobYub.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using Microsoft.Spatial;
using GeoCoordinatePortable;

namespace JobYub.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
       
        
        private readonly ApplicationDbContext _context;
        public AdvertisementsController(ApplicationDbContext context)
        {
            _context = context;
           
        }


        // GET: api/Advertisements
       
        [HttpGet]
        public async Task<ActionResult> GetAdvertisement(int? cityId, int page = 1)
        {
            //var user=_context.Users.Where(u=>u.Id== HttpContext.User.Identity.Name);
            //IQueryable<Advertisement> adsvertisements= _context.Advertisement.AsQueryable();
            //var kk = ("Administrators");
            //var bbb = HttpContext.User.IsInRole("MODERATORS");
            //var aaa = HttpContext.User.IsInRole("Administrators");
            //if (!HttpContext.User.IsInRole("Administrators") && !HttpContext.User.IsInRole("Moderators"))
            //	adsvertisements=_context.Advertisement.Where(a => a.ApplicationUserID == HttpContext.User.Identity.Name).AsQueryable();

            IQueryable<Advertisement> res = _context.Advertisement.Include(s => s.City).Include(s => s.Tarrif).Include(s => s.Region).Include(s => s.AdvertisementMajors).Include(s => s.AdvertisementEducationLevels).OrderBy(s=>s.StartDate);
            res = User.IsInRole("Administrators") ? res : res.Where(s => s.status == Status.confirmed);
            if (cityId != null && cityId != 0)
                res = res.Where(a => a.CityID == cityId);
            List<Advertisement> result = await res.Skip((page - 1) * 15).Take(15).ToListAsync();
            if (result != null)
                return Ok(new { Total = res.Count(), result = result });
            else
                return NotFound();

        }

        // GET: api/Advertisements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
		{
            var result = _context.Advertisement.Include(a => a.City).Include(a => a.ApplicationUser).ThenInclude(u => u.CompanyType).Include(a => a.ApplicationUser).ThenInclude(u => u.City).Include(a => a.ApplicationUser).ThenInclude(u => u.Major).Include(a => a.ApplicationUser).ThenInclude(u => u.EducationLevel).Include(a => a.ApplicationUser).ThenInclude(u => u.CompanyType).Include(a => a.JobCategory).Include(a => a.Payment).Include(a => a.Region).Include(a => a.Tarrif).Include(a => a.AdvertisementEducationLevels).ThenInclude(ael => ael.EducationLevel).Include(a => a.AdvertisementMajors).ThenInclude(am => am.Major).Include(a => a.AdvertisementCompanyTypes).ThenInclude(ac => ac.CompanyType);

            // result = await result.FirstOrDefaultAsync(a => a.ID == id);

            var advertisement = await result.FirstOrDefaultAsync(a => a.ID == id);
            if((advertisement.ApplicationUserID==User.Identity.Name|| User.IsInRole("Adminstrators")) &&advertisement!=null)
            {

                return advertisement;
            }

            else if (!User.IsInRole("Adminstrators")&&(advertisement.status==Status.confirmed) && advertisement != null)
            {

                return advertisement;
            }
            else
            {
                return NotFound();
            }

            
        }


		// POST: api/Advertisements
		[HttpPost]
		public async Task<ActionResult<Advertisement>> PostAdvertisement(Advertisement advertisement)
		{

			ApplicationUser u = _context.ApplicationUser.Find(HttpContext.User.Identity.Name);
			advertisement.ApplicationUser = u;

			_context.Advertisement.Add(advertisement);

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAdvertisement", new { id = advertisement.ID }, advertisement);
        }

		// PUT: api/Advertisements/5

		[HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(int id, Advertisement advertisement)
        {
            if (id != advertisement.ID)
            {
                return BadRequest();
            }
            Advertisement adInDb = await _context.Advertisement.FirstOrDefaultAsync(a => a.ID == advertisement.ID);
           // ApplicationUser u = _context.ApplicationUser.Where(h => h.Id == User.Identity.Name).Include(g => g.Advertisements).FirstOrDefault();
            if (adInDb == null)
                return NotFound();

            if (!User.IsInRole("Administrators"))
            {
              
                if ( adInDb.ApplicationUserID != User.Identity.Name)
                {
                   
               
                    return Unauthorized();
                }
            }

            _context.Entry(adInDb).State = EntityState.Detached;
            try
            {

                _context.Entry(advertisement).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //advertisement.a
                _context.AdvertisementMajors.RemoveRange(_context.AdvertisementMajors.Where(adm => adm.AdvertisementID == advertisement.ID));
                await _context.SaveChangesAsync();
                //advertisement.AdvertisementMajors.ForEach(am => am.Advertisement = advertisement);
                if (advertisement.AdvertisementMajors != null)
                    await _context.AdvertisementMajors.AddRangeAsync(advertisement.AdvertisementMajors);
                await _context.SaveChangesAsync();
                _context.AdvertisementEducationLevels.RemoveRange(_context.AdvertisementEducationLevels.Where(adm => adm.AdvertisementID == advertisement.ID));
                await _context.SaveChangesAsync();
                //advertisement.AdvertisementEducationLevels.ForEach(ae => ae.Advertisement = advertisement);
                if (advertisement.AdvertisementEducationLevels != null)
                    await _context.AdvertisementEducationLevels.AddRangeAsync(advertisement.AdvertisementEducationLevels);
                await _context.SaveChangesAsync();
				_context.AdvertisementCompanyTypes.RemoveRange(_context.AdvertisementCompanyTypes.Where(adc => adc.AdvertisementID == advertisement.ID));
                await _context.SaveChangesAsync();
                //advertisement.AdvertisementMajors.ForEach(am => am.Advertisement = advertisement);
                if (advertisement.AdvertisementCompanyTypes != null)
                    await _context.AdvertisementCompanyTypes.AddRangeAsync(advertisement.AdvertisementCompanyTypes);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // return NoContent();
            return Ok(advertisement);
        }
        
		// POST: api/UserAdvertisements
		[Route("/api/UserAdvertisements")]
		[HttpPost]
        public async Task<ActionResult<Advertisement>> UserAdvertisements(ApplicationUser user)
        {
			try
			{
				var advertisement = await _context.Advertisement.Where(a => a.ApplicationUserID == user.Id).Include(a => a.City).Include(a => a.JobCategory).Include(a => a.Payment).Include(a => a.Tarrif).ToListAsync();
				if (advertisement != null)
				{
					return Ok(advertisement);
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}
        
		// GET: api/UserAdvertisement
		//[Route("/api/UserAdvertisement")]
		//[HttpGet("{id}")]
		//public async Task<ActionResult<Advertisement>> UserAdvertisement(int id)
		//{
		//	var result = _context.Advertisement.Where(a => a.ID == id).Include(a => a.City).Include(a => a.ApplicationUser).Include(a => a.JobCategory).Include(a => a.Payment).Include(a => a.Region).Include(a => a.Tarrif).Include(a => a.AdvertisementEducationLevels).ThenInclude(ael => ael.EducationLevel).Include(a => a.AdvertisementMajors).ThenInclude(am => am.Major);
		//	// result = await result.FirstOrDefaultAsync(a => a.ID == id);
		//	var advertisement = await result.FirstOrDefaultAsync();


		//	if (advertisement == null)
		//	{
		//		return NotFound();
		//	}

		//	return advertisement;
		//}

		// DELETE: api/Advertisements/5

		[HttpDelete("{id}")]
        public async Task<ActionResult<Advertisement>> DeleteAdvertisement(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);
            ApplicationUser u = _context.ApplicationUser.Find(User.Identity.Name);
            
                if (advertisement == null||(!u.Advertisements.Contains(advertisement)&&!User.IsInRole("Administrators")))
            {
                return NotFound();
            }

            _context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();

            return advertisement;
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.ID == id);
        }
        
        [HttpPost]
        [Route("/Advertisements/search")]
        public async Task<IActionResult> SearchAsync(AdvertisementSearchModel model, int page = 1 ,string orderBy="date")
        {
               
            //IQueryable<Advertisement> res = _context.Advertisement;

            var query = _context.Advertisement.Where(a=>a.status==Status.confirmed).Include(s => s.City).Include(s => s.Tarrif).Include(s => s.Region).Include(s => s.AdvertisementMajors).Include(s => s.AdvertisementEducationLevels).Include(a => a.AdvertisementCompanyTypes).ThenInclude(ac => ac.CompanyType).AsQueryable();
            query = query.Where(a => a.status == Status.confirmed);         
            if (model.AdvertisementType != null)
                query = query.Where(a => a.advertisementType == model.AdvertisementType);

            if (model.CollaborationType != null)
                query = query.Where(a => a.CollaborationType == model.CollaborationType);

            if (model.MinSalary != null)
                query = query.Where(a => a.MinSalary >= model.MinSalary);

            if (model.MaxSalary != null)
                query = query.Where(a => a.MaxSalary <= model.MaxSalary);

            if (model.MinAge != null)
                query = query.Where(a => a.MinAge >= model.MinAge);

            if (model.MaxAge != null)
                query = query.Where(a => a.MaxAge <= model.MaxAge);


            if (model.Gender != null)
                query = query.Where(a => a.Gender == model.Gender);

            if (model.CompanyTypeIDs != null)
            {
				//model.CompanyTypeIDs.ForEach(cID => query = query.Where(a => a.ApplicationUser.CompanyTypeID == cID));
				query = query.Where(a => model.CompanyTypeIDs.Contains(a.ApplicationUser.CompanyTypeID));
			}
            
            if(model.Graduated!=null)
                query = query.Where(a => a.Graduated == model.Graduated);

            if (model.EducationLevelIDs != null)
            {
				query = query.Where(a => a.AdvertisementEducationLevels.Where(ael=>model.EducationLevelIDs.Contains(ael.EducationLevelID)).Count()!=0);
               // model.EducationLevelIDs.ForEach(eID => query = query.Where(a => a.AdvertisementEducationLevels.Where(ae=>ae.EducationLevelID==eID)!=null));

            }

            if (model.MajorIDs != null)
            {
				query = query.Where(a => a.AdvertisementMajors.Where(am =>model.MajorIDs.Contains(am.MajorID)).Count() != 0);
			}         
            if (model.Experience != null)
                query = query.Where(a => a.Experience >= model.Experience);
            
            if (model.KeyWord != null)
                query = query.Where(a => a.Title.Contains(model.KeyWord) || a.Description.Contains(model.KeyWord));

            if (model.Title != null)
                query = query.Where(a => a.Title.Contains(model.Title) == true);

            if (model.City != null)
                query = query.Where(a => a.City.Name.Contains(model.City) == true);

            if (model.JobCategoryID != null)
                query = query.Where(a => a.JobCategory.ID == model.JobCategoryID);

			Dictionary<Advertisement, double?> advertisementDist = new Dictionary<Advertisement, double?>();
			
			switch (orderBy)
			{
				case "distance":
					{
						if (model.Latitude != null && model.Longitude != null)
						{
							var userLocation= new GeoCoordinate(Convert.ToDouble(model.Latitude), Convert.ToDouble(model.Longitude));
							//var userLocation = GeographyPoint.Create(Convert.ToDouble(model.Latitude), Convert.ToDouble(model.Longitude));
							foreach (Advertisement ads in query)
							{
								if (ads.Latitude != null && ads.Longitude != null)
								{
									var advertisementLocation = new GeoCoordinate(Convert.ToDouble(ads.Latitude), Convert.ToDouble(ads.Longitude));
									//var advertisementLocation = GeographyPoint.Create(Convert.ToDouble(ads.Latitude), Convert.ToDouble(ads.Longitude));
									double? dd = userLocation.GetDistanceTo(advertisementLocation);
									//double? dd=GeographyOperationsExtensions.Distance(userLocation, advertisementLocation);
									//ge
									//double? dd = userLocation.Distance(advertisementLocation).Value;
									advertisementDist.Add(ads, dd);
								}
								else
								{
									advertisementDist.Add(ads, double.MaxValue);
								}
							}
							advertisementDist = advertisementDist.OrderBy(a => a.Value).ToDictionary(z => z.Key, y => y.Value); 
						
						}
						else
						{
							advertisementDist = query.OrderByDescending(a => a.StartDate).ToDictionary(x => x, x => (double?)0.0);
						}
						
						break;
					}
				case "date":
					{
						//advertisementDist = new Dictionary<Advertisement, double?>();
						advertisementDist= query.OrderByDescending(a => a.StartDate).ToDictionary(x => x, x =>  (double?)0.0);
						break;
					}
			}
			int total = advertisementDist.Count();
			advertisementDist =  advertisementDist.Skip((page - 1) * 15).Take(15).ToDictionary(z => z.Key, y => y.Value); 
			//return Ok(await query.ToListAsync());
			return Ok(new { total=total, advertisments = advertisementDist.Keys, distances = advertisementDist.Values });
		}
        
        [HttpPost]
        [Authorize(Roles ="Administrators")]
        [Route("/Advertisements/Confirm")]
		public async Task<ActionResult> ConfirmAdvertisements(AdvertisementIDsModel advertisementIDs)
		{
			try
			{
				foreach (int id in advertisementIDs.AdvertisementIDs)
				{
					var advertisement = await _context.Advertisement.FindAsync(id);
					advertisement.status = Status.confirmed;
				}
				await _context.SaveChangesAsync();
				return Ok();
			}
			catch(Exception ex)
			{
				return StatusCode(500);
			}

		}
        
        
        [Authorize(Roles ="Administrators")]
		[Route("/Advertisements/Deactivate")]
		[HttpPost]
		public async Task<ActionResult> DeactivateAdvertisements(AdvertisementIDsModel advertisementIDs)
		{
			try
			{
				foreach (int id in advertisementIDs.AdvertisementIDs)
				{
					var advertisement = await _context.Advertisement.FindAsync(id);
					advertisement.status = Status.deactive;
				}
				await _context.SaveChangesAsync();
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}

		}

    }
}
