using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Contracts.V1;
using MyWebApi.Contracts.V1.Requests;
using MyWebApi.Contracts.V1.Responses;
using MyWebApi.Domain;
using MyWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Controllers.V1
{
    [EnableCors("CorsPolicy")]
    public class OwnersController : Controller
    {
        private readonly IOwnerService _ownerService;
        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet(ApiRoutes.Owners.GetAll)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ownerService.GetOwnersAsync());
        }

        [HttpPut(ApiRoutes.Owners.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid ownerId, [FromBody] UpdateOwnerRequest request)
        {
            var owner = new Owner
            {
                Id = ownerId,
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address
            };

            var updated = await _ownerService.UpdateOwnerAsync(owner);

            if (updated)
                return Ok(owner);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Owners.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid ownerId)
        {
            var deleted = await _ownerService.DeleteOwnerAsync(ownerId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpGet(ApiRoutes.Owners.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid ownerId)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(ownerId);

            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpGet(ApiRoutes.Owners.GetWithDetails)]
        public async Task<IActionResult> GetOwnerWithDetails(Guid ownerId)
        {
            var owner = await _ownerService.GetOwnerWithDetailsAsync(ownerId);

            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        [HttpPost(ApiRoutes.Owners.Create)]
        public async Task<IActionResult> Post([FromBody] CreateOwnerRequest ownerRequest)
        {
            var owner = new Owner 
            { 
                Name = ownerRequest.Name,
                DateOfBirth = ownerRequest.DateOfBirth,
                Address = ownerRequest.Address
            };

            await _ownerService.CreateOwnerAsync(owner);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent().ToString()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Owners.Get.Replace("{ownerId}", owner.Id.ToString());

            var response = new OwnerResponse { Id = owner.Id };

            return Created(locationUrl, response);
        }


    }
}
