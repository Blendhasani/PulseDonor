using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PulseDonor.Application.Account.Commands;
using PulseDonor.Application.Account.DTO;
using PulseDonor.Application.Account.Interfaces;
using PulseDonor.Application.CurrentUser.Interface;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.Services
{
	public class AccountService : IAccountService
	{
		private readonly DevPulsedonorContext _context;
		private readonly ICurrentUser _currentUser;

		public AccountService(DevPulsedonorContext context, ICurrentUser currentUser)
		{
			_context = context;
			_currentUser = currentUser;
		}

		



		public async Task<SingleAccountOverviewDto> GetAccountOverviewAsync()
		{
			var user = await _context.Users
				.Include(x=>x.BloodType)
				.Include(x=>x.UserCities)
					.ThenInclude(x=>x.City)
				.Where(x => x.Id == _currentUser.UserId)
				.FirstOrDefaultAsync();

			var data = new SingleAccountOverviewDto
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				BloodType = new SingleBloodTypeDto { BloodTypeId = user.BloodTypeId, Type = user.BloodType.Type },
				PrimaryCity = user.UserCities.Where(x => x.IsPrimary && !x.IsDeleted).Select(x => new SinglePrimaryCityDto { PrimaryCityId = x.CityId, Name = x.City.Name }).FirstOrDefault(),
				SecondaryCities = user.UserCities.Where(x => !x.IsPrimary && !x.IsDeleted).Any()? 
				user.UserCities.Where(x => !x.IsPrimary && !x.IsDeleted).Select(x => new SingleSecondaryCityDto { SecondaryCityId = x.CityId, Name = x.City.Name }).ToList() : new List<SingleSecondaryCityDto>(),
				Status = user.IsActive,
			};

			return data;
		}
		public async Task<string> EditAccountOverviewAsync(EditAccountOverviewCommand cmd)
		{
			var user = await _context.Users
				.Include(x => x.BloodType)
				.Include(x => x.UserCities)
					.ThenInclude(x => x.City)
				.Where(x => x.Id == _currentUser.UserId)
				.FirstOrDefaultAsync();

			if (user == null)
			{
				throw new Exception("User not found.");
			}

			user.FirstName = cmd.FirstName;
			user.LastName = cmd.LastName;
			//user.Email = cmd.Email;
			user.IsActive = cmd.Status;
			user.BloodTypeId = cmd.BloodTypeId;

			if (cmd.PrimaryCityIdToDelete != null)
			{
				var previouslyUserPrimaryCity = await _context.UserCities
					.Where(x => x.IsPrimary && !x.IsDeleted && x.UserId == _currentUser.UserId && x.CityId == cmd.PrimaryCityIdToDelete)
					.FirstOrDefaultAsync();

				if (previouslyUserPrimaryCity != null)
				{
					previouslyUserPrimaryCity.IsDeleted = true;
				}

				var newPrimaryCity = new UserCity
				{
					UserId = _currentUser.UserId,
					CityId = cmd.PrimaryCityId,
					IsPrimary = true,
					InsertedFrom = _currentUser.UserId,
					InsertedDate = DateTime.UtcNow,
					IsDeleted = false
				};

				await _context.UserCities.AddAsync(newPrimaryCity);
			}

			if (cmd.SecondaryCityIdsToDelete != null)
			{
				var previouslyUserCities = await _context.UserCities
					.Where(x => x.UserId == _currentUser.UserId && !x.IsPrimary && !x.IsDeleted)
					.ToListAsync();

				foreach (var city in previouslyUserCities)
				{
					if (cmd.SecondaryCityIdsToDelete.Contains(city.CityId))
					{
						city.IsDeleted = true;
					}
				}
			}

			if (cmd.SecondaryCities != null)
			{
				foreach (var cityId in cmd.SecondaryCities)
				{
					var newSecondaryCity = new UserCity
					{
						UserId = _currentUser.UserId,
						CityId = cityId,
						IsPrimary = false,
						InsertedFrom = _currentUser.UserId,
						InsertedDate = DateTime.UtcNow,
						IsDeleted = false
					};

					await _context.UserCities.AddAsync(newSecondaryCity);
				}
			}

			await _context.SaveChangesAsync();

			return user.Id;
		}


		public async Task<List<GetUserProfileBloodRequestsDto>> GetMyBloodRequestsAsync()
		{
			var bloodRequests = _context.BloodRequests
				.Include(x => x.BloodType)
				.Include(x => x.UrgenceType)
				.Include(x=> x.Hospital)
				.Include(x=>x.BloodRequestApplications)
				.Where(x=> !x.IsDeleted && x.AuthorId == _currentUser.UserId)
				.AsQueryable();

			var response = await bloodRequests.Select(x => new GetUserProfileBloodRequestsDto
			{
				Id = x.Id,
				BloodType = x.BloodType.Type,
				Quantity = x.Quantity,
				UrgenceType = new SingleUserProfileUrgencyType
				{
					Id = x.UrgenceTypeId,
					Type = x.UrgenceType.Type,
				},
				DonationDate = x.DonationDate != null ? x.DonationDate.Value : null,
				DonationTime = x.DonationTime != null ? x.DonationTime.Value : null,
				Hospital = x.HospitalId != null ? new SingleUserProfileHospital
				{
					Id = x.HospitalId,
					Name = x.Hospital.Name
				} : null,
				NumberOfApplications = x.BloodRequestApplications.Where(y => !y.IsDeleted).Count(),

			}).ToListAsync();

			return response;

		}

		public async Task<int> CreateBloodRequestPostAsync(AddBloodRequestCommand cmd)
		{
			var currentUser = await _context.Users.Where(x => x.Id == _currentUser.UserId).FirstOrDefaultAsync();
			var postKey = GeneratePostId(currentUser.FirstName, currentUser.LastName);

			var newBloodRequest = new BloodRequest
			{
				BloodTypeId = cmd.BloodTypeId,
				Quantity = cmd.Quantity,
				UrgenceTypeId = cmd.UrgenceTypeId,
				HospitalId = cmd.HospitalId,
				DonationDate = cmd.DonationDate,
				DonationTime = cmd.DonationTime,
				FirstName = cmd.FirstName,
				LastName = cmd.LastName,
				Age = cmd.Age,
				PostKey = postKey,
				AuthorId = _currentUser.UserId,
				InsertedDate = DateTime.UtcNow,
				InsertedFrom = _currentUser.UserId,
				IsDeleted = false
			};

			await _context.BloodRequests.AddAsync(newBloodRequest);	
			await _context.SaveChangesAsync();
			return newBloodRequest.Id;


		}

		private string GeneratePostId(string firstName, string lastName)
		{
			//Initials from first and last name
			var initials = $"{firstName[0]}{lastName[0]}".ToUpper();

			//4 random digits + 2 random letters
			var random = new Random();
			var digits = random.Next(1000, 9999); 
			var letters = Path.GetRandomFileName().ToUpper().Substring(0, 2); // 2 random letters

			return $"{initials}-{digits}{letters}";
		}

		public async Task<SingleBloodRequestDto> GetBloodRequestByIdAsync(int id)
		{
			if(id == 0)
			{
				return new SingleBloodRequestDto();
			}

			var bloodRequest = _context.BloodRequests
				.Include(x => x.BloodType)
				.Include(x => x.UrgenceType)
				.Include(x => x.Hospital)
				.Include(x => x.BloodRequestApplications)
				.Where(x => !x.IsDeleted)
				.AsQueryable();

			if (bloodRequest is null)
			{
				return new SingleBloodRequestDto();
			}

			var response = await bloodRequest.Select(x => new SingleBloodRequestDto
			{
				Id = x.Id,
				BloodType = new SingleBRBloodTypeDto
				{
					BloodTypeId = x.BloodTypeId,
					Type = x.BloodType.Type
				},
				Quantity = x.Quantity,
				UrgenceType = new SingleBRUrgenceDto
				{
					UrgenceTypeId = x.UrgenceTypeId,
					Type = x.UrgenceType.Type
				},
				Hospital = x.HospitalId != null ? new SingleBRHospitalDto
				{
					HospitalId = x.HospitalId,
					Name = x.Hospital.Name
				} : null,
				DonationDate = x.DonationDate,
				DonationTime = x.DonationTime,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Age = x.Age
			}).FirstOrDefaultAsync();

			return response;
		}

		public async Task<int> EditBloodRequestPostAsync(int id, EditBloodRequestCommand cmd)
		{
			if(id == 0)
			{
				return 0;
			}

			var bloodRequest = await _context.BloodRequests
							.Include(x => x.BloodType)
							.Include(x => x.UrgenceType)
							.Include(x => x.Hospital)
							.Include(x => x.BloodRequestApplications)
							.Where(x => !x.IsDeleted)
							.FirstOrDefaultAsync();
			if (bloodRequest is null)
			{
				return 0;
			}

			bloodRequest.BloodTypeId = cmd.BloodTypeId;
			bloodRequest.Quantity = cmd.Quantity;
			bloodRequest.UrgenceTypeId = cmd.UrgenceTypeId;
			bloodRequest.HospitalId = cmd.HospitalId;
			bloodRequest.DonationDate = cmd.DonationDate;
			bloodRequest.DonationTime = cmd.DonationTime;
			bloodRequest.FirstName = cmd.FirstName;
			bloodRequest.LastName = cmd.LastName;
			bloodRequest.Age = cmd.Age;

			await _context.SaveChangesAsync();
			return bloodRequest.Id;
		}

		public async Task<string> DeleteBloodRequestPostAsync(int id)
		{
			if (id == 0)
			{
				return "Kerkesa deshtoi!";
			}

			var bloodRequest = await _context.BloodRequests.Where(x => x.Id == id).FirstOrDefaultAsync();

			if(bloodRequest is null)
			{
				return "Kerkesa deshtoi!";
			}

			bloodRequest.IsDeleted = true;

			await _context.SaveChangesAsync();
			return "Kerkesa u realizua!";
		}

		public async Task<List<GetUserProfileApplicationsDto>> GetMyApplicationsAsync()
		{
			var bloodRequestsApplications = _context.BloodRequestApplications
				.Include(x => x.BloodRequest)
					.ThenInclude(x=>x.UrgenceType)
				.Include(x => x.BloodRequest)
					.ThenInclude(x => x.BloodType)
				.Where(x => x.UserId == _currentUser.UserId)
				.AsQueryable();

			var response = await bloodRequestsApplications.Select(x => new GetUserProfileApplicationsDto
			{
				Id = x.Id,
				BloodRequestId = x.BloodRequestId,
				PostKey = x.BloodRequest.PostKey,
				Quantity = x.BloodRequest.Quantity,
				Urgence = new SingleApplicationUrgenceDto
				{
					UrgenceTypeId = x.BloodRequest.UrgenceTypeId,
					UrgenceType = x.BloodRequest.UrgenceType.Type
				},
				BloodType = new SingleApplicationBloodTypeDto
				{
					BloodTypeId = x.BloodRequest.BloodTypeId,
					BloodType = x.BloodRequest.BloodType.Type
				},
				DonationDate = x.BloodRequest.DonationDate,
				DonationTime = x.BloodRequest.DonationTime,
				IsAccepted = x.BloodRequest.DonorId == _currentUser.UserId ? true : x.BloodRequest.DonorId == null? null : false

			}).ToListAsync();

			return response;
		}

		public async Task<SingleUserApplicationDto> GetApplicationByIdAsync(int id)
		{
			if (id == 0)
			{
				return new SingleUserApplicationDto();
			}

			var application = _context.BloodRequestApplications
				.Include(x=>x.BloodRequest)
				.ThenInclude(x=> x.UrgenceType)
				.Include(x=>x.BloodRequest)
				.ThenInclude(x=>x.BloodType)
				.Where(x=>x.Id == id)
				.AsQueryable();

			var response = await application.Select(x=> new SingleUserApplicationDto {
			Id = x.Id,
			PostKey = x.BloodRequest.PostKey,
			Quantity = x.BloodRequest.Quantity,
			Urgence = new SingleApplicationUrgenceType
			{
				UrgenceTypeId = x.BloodRequest.UrgenceTypeId,
				UrgenceType = x.BloodRequest.UrgenceType.Type,
			},
			BloodType = new SingleApplicationBloodType
			{
				BloodTypeId = x.BloodRequest.BloodTypeId,
				BloodType = x.BloodRequest.BloodType.Type,
			},
			DonationDate = x.BloodRequest.DonationDate,
			DonationTime = x.BloodRequest.DonationTime,
			IsAccepted = x.BloodRequest.DonorId == _currentUser.UserId ? true : x.BloodRequest.DonorId == null ? null : false

			}).FirstOrDefaultAsync();

			return response;
		}

		public async Task<string> DeleteApplicationAsync(int id)
		{
			if (id == 0)
			{
				return "Kërkesa u deshtoi!";
			}

			var bloodApplication = await _context.BloodRequestApplications
				.Include(x=>x.BloodRequest)
				.Where(x => x.Id == id).FirstOrDefaultAsync();

			if (bloodApplication == null)
			{
				return "Nuk u gjet";
			}
			
			if (bloodApplication.BloodRequest != null)
			{
				bloodApplication.BloodRequest.DonorId = null;
			}

			bloodApplication.IsDeleted = true;
			await _context.SaveChangesAsync();
			return "Kërkesa u plotësua!";

		}
	}
}
