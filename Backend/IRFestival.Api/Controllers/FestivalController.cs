using System.Net;

using Microsoft.AspNetCore.Mvc;

using IRFestival.Api.Data;
using IRFestival.Api.Domain;
using IRFestival.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.ApplicationInsights;

namespace IRFestival.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FestivalController : ControllerBase
    {
        FestivalDbContext context;
        TelemetryClient _telemetry;

        public FestivalController(FestivalDbContext ctx, TelemetryClient telemetry)
        {
            context = ctx;
            _telemetry = telemetry;
        }

        [HttpGet("LineUp")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Schedule))]
        public async Task<ActionResult> GetLineUp()
        {
            return Ok(await context.Schedules.ToListAsync());
        }

        [HttpGet("Artists")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Artist>))]
        public async Task<ActionResult> GetArtists(bool? withRatings)
        {
            if (withRatings.HasValue && withRatings.Value)
            {
                _telemetry.TrackEvent("List of artists with ratings");
            } else
            {
                _telemetry.TrackEvent("List of artists without ratings");
            }
            var artists = await context.Artists.ToListAsync();
            return Ok(artists);
        }

        [HttpGet("Stages")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<Stage>))]
        public async Task<ActionResult> GetStages()
        {
            return Ok(await context.Stages.ToListAsync());
        }

        [HttpPost("Favorite")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ScheduleItem))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> SetAsFavorite(int id)
        {
            var items = await context.ScheduleItems.ToListAsync();
            var schedule = items
                .FirstOrDefault(si => si.Id == id);
            if (schedule != null)
            {
                schedule.IsFavorite = !schedule.IsFavorite;
                return Ok(schedule);
            }
            return NotFound();
        }

    }
}