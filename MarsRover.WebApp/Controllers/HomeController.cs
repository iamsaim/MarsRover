using MarsRover.Application.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using MarsRover.Application.Common.ViewModel;
using ExcelDataReader;
using Newtonsoft.Json;
using MarsRover.Domain.Constants;
using MarsRover.Infrastructure.Extension;

namespace MarsRover.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlateauService _plateauService;
        private readonly IMarsRoverService _roverService;
        public HomeController(IPlateauService plateauService,
            IMarsRoverService roverService)
        {
            _plateauService = plateauService;
            _roverService = roverService;
        }
        public async Task<IActionResult> Index(Guid PlateauId)
        {
            var IfExists = await _plateauService.CheckIfExists(new CancellationToken());
            if (!IfExists || PlateauId == Guid.Empty)
                return RedirectToAction("InitiatePlateau", new { IfExists = false });
            
            var plateau = await _plateauService.GetPlateau(PlateauId, new CancellationToken());
            return View(plateau);
        }

        public async Task<IActionResult> InitiatePlateau(bool IfExists)
        {
            if (IfExists)
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InitiatePlateau(InitiatePlateauVM initiatePlateauDTO)
        {
            if (!ModelState.IsValid)
                return View(initiatePlateauDTO);

            var PlateauId = await _plateauService.InitiatePlateau(initiatePlateauDTO, new CancellationToken());
            return RedirectToAction("Index", new { PlateauId = PlateauId });
        }

        [HttpPost]
        public async Task<IActionResult> DeployRover([FromBody] object model)
        {
            string json = model.ToString();
            var deployRoverVM = JsonConvert.DeserializeObject<DeployRoverVM>(json);

            var Rover = await _roverService.DeployRovers(deployRoverVM, new CancellationToken());

            if (Rover == Guid.Empty)
                return BadRequest();
            return new JsonResult(deployRoverVM.PlateauId);
        }

        [HttpPost]
        public async Task<IActionResult> MoveRover([FromBody] object model)
        {
            string json = model.ToString();
            var moveRoverVM = JsonConvert.DeserializeObject<MoveRoverVM>(json);

            var moved = await _roverService.MoveRover(moveRoverVM, new CancellationToken());

            if (!moved)
                return BadRequest();
            return new JsonResult(moved);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCSV([FromForm] IFormFile file, [FromForm] string plateauId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                Stream myStream = ms;
                using (var reader = ExcelReaderFactory.CreateCsvReader(myStream))
                {
                    int count = 0;
                    while (reader.Read() && reader.GetValue(0) != null)
                    {

                        try
                        {
                            var initiate = reader.GetValue(0).ToString().Split(" ");
                            string moves = string.Empty;
                            if(reader.GetValue(1) != null)
                                moves = reader.GetValue(1).ToString();

                            var roverId = await _roverService.DeployRovers(new DeployRoverVM
                            {
                                CardinalPoint = (CardinalPoint)System.Enum.Parse(typeof(CardinalPoint), initiate[2]),
                                x = Convert.ToInt32(initiate[0]),
                                y = Convert.ToInt32(initiate[1]),
                                PlateauId = plateauId.ToGuid(),
                                Name = count.ToString(),
                            }, new CancellationToken());

                            await _roverService.MoveRover(new MoveRoverVM { Id = roverId, Moves = moves }, new CancellationToken());

                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                        
                        count++;


                    }
                }
            }

            return Ok();
        }
    }
}
