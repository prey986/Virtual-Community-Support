using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);
        Task<bool> UpdateMission(Missions missions);
        string DeleteMission(int id);
        Task<IList<Missions>> ClientSideMissionList();
        List<DropDownResponseModel> MissionThemeList();
        List<DropDownResponseModel> MissionSkillList();
        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        List<MissionApplicationResponse> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);

    }
}
