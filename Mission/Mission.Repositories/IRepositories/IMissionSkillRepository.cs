using Mission.Entities.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionSkillRepository
    {
        Task<bool> AddMissionSkill(MissionSkill missionSkill);
        Task<bool> DeleteMissionSkill(int missionSkillid);
        Task<List<MissionSkillViewModel>> GetAllMissionSkill();
        Task<MissionSkillViewModel?> GetMissionSkillById(int missionSkillId);
        Task<bool> UpdateMissionSkill(MissionSkill missionSkill);
        String GetMissionSkills(string skillIds);
    }
}
