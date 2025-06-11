using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Entities.Models.CommonModels;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(AddMissionRequestModel model);
        Task<bool> UpdateMission(UpdateMission model);
        string MissionDelete(int id);
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId);
        List<DropDownResponseModel> MissionThemeList();
        List<DropDownResponseModel> MissionSkillList();
        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);
        List<MissionApplicationResponse> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);

    }
}
