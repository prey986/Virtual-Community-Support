using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Mission.Services.Services;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController(IMissionSkillServices missionSkillServices) : ControllerBase
    {
        private readonly IMissionSkillServices _missionSkillServices=missionSkillServices;
        [HttpGet]
        [Route("GetMissionSkillList")]
        public async Task<IActionResult> GetAllMissionSkill()
        {
            try
            {
                var res = await _missionSkillServices.GetAllMissionSkill();
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to get mission Skill" });
            }
        }
        [HttpPost]
        [Route("AddMissionSkill")]
        public async Task<IActionResult> AddMissionSkill(MissionSkillViewModel missionSkillViewModel)
        {
            try
            {
                var res = await _missionSkillServices.AddMissionSkill(missionSkillViewModel);
                return Ok(new ResponseResult() { Data = "Add Mission Skill.", Result = ResponseStatus.Success, Message = "MissionSkill Added successfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add mission skill" });
            }
        }
        [HttpGet]
        [Route("GetMissionSkillById/{id:int}")]
        public async Task<IActionResult> GetMissionSkillById(int id)
        {
            var res = await _missionSkillServices.GetMissionSkillById(id);

            if (res == null)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error, Message = "" });

            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
        }
        [HttpPost]
        [Route("UpdateMissionSkill")]
        public async Task<IActionResult> UpdateMissionSkill(MissionSkillViewModel missionSkillViewModel)
        {
            var res = await _missionSkillServices.UpdateMissionSkill(missionSkillViewModel);

            if (!res)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error, Message = "" });

            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Updated Successfully" });
        }
        [HttpDelete]
        [Route("DeleteMissionSkill/{id:int}")]
        public async Task<IActionResult> DeleteMissionSkill(int id)
        {
            var res = await _missionSkillServices.DeleteMissionSkill(id);
            if (!res)
                return NotFound(new ResponseResult() { Data = "Not Found", Result = ResponseStatus.Error, Message = "" });
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "MissionSkill Deleted Succesfully" });
        }
    }
}
