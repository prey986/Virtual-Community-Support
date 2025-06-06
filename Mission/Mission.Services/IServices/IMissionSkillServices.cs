using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IMissionSkillServices
    {
        Task<bool> AddMissionSkill(MissionSkillViewModel model);
        Task<bool> DeleteMissionSkill(int missionSkillId);
        Task<List<MissionSkillViewModel>> GetAllMissionSkill();
        Task<MissionSkillViewModel?> GetMissionSkillById(int missionSkillId);
        Task<bool> UpdateMissionSkill(MissionSkillViewModel model);

    }
}
