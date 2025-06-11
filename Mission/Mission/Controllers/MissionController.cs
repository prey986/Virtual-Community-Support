using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services;
using Mission.Services.IServices;
using System.Threading.Tasks;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpPost]
        [Route("AddMission")]
        public async Task<IActionResult> AddMission(AddMissionRequestModel model)
        {
            var response = await _missionService.AddMission(model);
            return Ok(new ResponseResult()
            {
                Data = response,
                Result = ResponseStatus.Success,
                Message = ""
            });
        }
        [HttpPost]
        [Route("UpdateMission")]
        public async Task<IActionResult> UpdateMission(UpdateMission model)
        {
            var res = await _missionService.UpdateMission(model);
            if (!res)
                return NotFound(new ResponseResult() { Data = "NotFound", Result = ResponseStatus.Error, Message = "Unable to update" });
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Mission Updated Successfully" });
        }
        [HttpDelete]
        [Route("DeleteMission/{id}")]
        public ActionResult DeleteMission(int id)
        {
            try
            {
                var res = _missionService.MissionDelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Mission Deleted Succesfully" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete Mission" });
            }
        }

        [HttpGet]
        [Route("MissionList")]
        public async Task<IActionResult> GetAllMissionAsync()
        {
            var response = await _missionService.GetAllMissionAsync();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await _missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }
        [HttpGet]
        [Route("GetMissionThemeList")]
        public IActionResult MissionThemeList()
        {
            var response = _missionService.MissionThemeList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }
        [HttpGet]
        [Route("GetMissionSkillList")]
        public IActionResult MissionSkillList()
        {
            var result = new ResponseResult();
            try
            {
                result.Data = _missionService.MissionSkillList();
                result.Result = ResponseStatus.Success;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public IActionResult MissionApplicationList()
        {
            var response = _missionService.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await _missionService.MissionApplicationApprove(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = "Approved successfully", Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
        [HttpPost]
        [Route("MissionApplicationDelete")]
        public async Task<IActionResult> MissionApplicationDelete(UpdateMissionApplicationModel missionApp)
        {
            try
            {
                var ret = await _missionService.MissionApplicationDelete(missionApp);
                return Ok(new ResponseResult() { Data = ret, Message = "Deleted Successfully", Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Message = ex.Message, Result = ResponseStatus.Error });
            }
        }
    }
}
