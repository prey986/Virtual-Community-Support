using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return _dbContext.Missions.Where(x=>!x.IsDeleted).Include(m => m.MissionTheme).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeName = m.MissionTheme.ThemeName,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            try
            {
                _dbContext.Missions.Add(mission);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }
        public async Task<bool> UpdateMission(Missions missions)
        {
            var missionexistindb = await _dbContext.Missions.FindAsync(missions.Id);
            if (missionexistindb == null)
                return false;
            missionexistindb.MissionTitle = missions.MissionTitle;
            missionexistindb.MissionDescription = missions.MissionDescription;
            missionexistindb.CountryId = missions.CountryId;
            missionexistindb.CityId = missions.CityId;
            missionexistindb.StartDate = missions.StartDate;
            missionexistindb.EndDate = missions.EndDate;
            missionexistindb.TotalSheets = missions.TotalSheets;
            missionexistindb.MissionThemeId = missions.MissionThemeId;
            missionexistindb.MissionSkillId = missions.MissionSkillId;
            missionexistindb.MissionImages = missions.MissionImages;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public string DeleteMission(int id)
        {
            var mission = _dbContext.Missions.Where(x => x.Id == id).FirstOrDefault();

            if (mission == null) throw new Exception("Mission does't exist!");

            mission.IsDeleted = true;

            //user.EmailAddress = model.EmailAddress

            mission.ModifiedDate = DateTime.Now;
            _dbContext.Missions.Update(mission);
            _dbContext.SaveChanges();
            return "Mission deleted!";
        }

        // int userId
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
               .Include(m => m.MissionApplications)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }
        public List<DropDownResponseModel> MissionThemeList()
        {
            var missionThemes = _dbContext.MissionThemes
                .Where(mt => mt.Status == "active")
                .Select(mt => new DropDownResponseModel(mt.Id, mt.ThemeName))
                .Distinct()
                .ToList();

            return missionThemes;
        }
        public List<DropDownResponseModel> MissionSkillList()
        {
            var missionSkill = _dbContext.MissionSkills
                .Where(ms => ms.Status == "active")
                .Select(ms => new DropDownResponseModel(ms.Id, ms.SkillName))
                .ToList();

            return missionSkill;
        }
        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var mission = _dbContext.Missions.Where(x => x.Id == model.MissionId).FirstOrDefault();

                if (mission == null) throw new Exception("Mission not found");

                var application = _dbContext.MissionApplications.Where(x => x.MissionId == model.MissionId && x.UserId == model.UserId).FirstOrDefault();

                if (application != null) throw new Exception("Already applied!");

                MissionApplication app = new MissionApplication()
                {
                    UserId = model.UserId,
                    MissionId = model.MissionId,
                    AppliedDate = model.AppliedDate,
                    Seats = model.Sheet,
                    Status = model.Status,

                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };

                mission.TotalSheets -= model.Sheet;

                await _dbContext.MissionApplications.AddAsync(app);
                _dbContext.Missions.Update(mission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<MissionApplicationResponse> GetMissionApplicationList()
        {
            return _dbContext.MissionApplications
                .Include(m => m.Mission)
                .Include(m => m.User)
                .Where(x => !x.IsDeleted)
                .Select(m => new MissionApplicationResponse
                {
                    Id = m.Id,
                    MissionTitle = m.Mission != null ? m.Mission.MissionTitle : "N/A",
                    MissionTheme = m.Mission != null && m.Mission.MissionTheme != null ? m.Mission.MissionTheme.ThemeName : "N/A",
                    UserName = m.User != null ? $"{m.User.FirstName} {m.User.LastName}" : "Unknown",
                    AppliedDate = m.AppliedDate,
                    Status = m.Status
                })
                .ToList();
        }



        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.IsDeleted = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
