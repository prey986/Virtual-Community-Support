using Microsoft.EntityFrameworkCore;
using Mission.Entities.Context;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionSkillRepository(MissionDbContext missionDbContext):IMissionSkillRepository
    {
        private readonly MissionDbContext _missionDbContext = missionDbContext;
        public async Task<bool> AddMissionSkill(MissionSkill missionSkill)
        {
            _missionDbContext.MissionSkills.Add(missionSkill);
            await _missionDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMissionSkill(int missionSkillid)
        {
            var missionskillexistindb = await _missionDbContext.MissionSkills.FindAsync(missionSkillid);
            if (missionskillexistindb == null)  return false;
            _missionDbContext.Remove(missionskillexistindb);
            await _missionDbContext.SaveChangesAsync();
            return true;
        }
        public Task<List<MissionSkillViewModel>> GetAllMissionSkill()
        {
            return _missionDbContext.MissionSkills.
                Select(m => new MissionSkillViewModel()
                {
                    Id = m.Id,
                    SkillName = m.SkillName,
                    Status = m.Status,
                })
                .ToListAsync();

        }
        public Task<MissionSkillViewModel?> GetMissionSkillById(int missionSkillId)
        {
            return _missionDbContext.MissionSkills
                .Where(m => m.Id == missionSkillId)
                .Select(m => new MissionSkillViewModel()
                {
                    Id = m.Id,
                    SkillName = m.SkillName,
                    Status= m.Status,
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateMissionSkill(MissionSkill missionSkill )
        {
            var missionskillexistinbd = await _missionDbContext.MissionSkills.FindAsync(missionSkill.Id);
            if (missionskillexistinbd == null)
                 return false; 
            missionskillexistinbd.SkillName = missionSkill.SkillName;
            missionskillexistinbd .Status = missionSkill.Status;
            await _missionDbContext.SaveChangesAsync();
            return true;

        }
        public string GetMissionSkills(string skillIds)
        {
            List<string> skillIdList = skillIds.Split(",").ToList();

            return string.Join(",", _missionDbContext.MissionSkills
                .Where(ms => skillIdList.Contains(ms.Id.ToString()))
                .Select(ms => ms.SkillName).ToList());
        }
    }
}
