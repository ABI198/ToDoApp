using ABISoft.ToDoAppNTier.Business.Interfaces;
using ABISoft.ToDoAppNTier.Common.ResponseObjects;
using ABISoft.ToDoAppNTier.Dtos.WorkDtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;
        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _workService.GetAllAsync();
            return View(response.Data);
        }

        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto workCreateDto)
        {
            var response = await _workService.AddAsync(workCreateDto);
            if(response.ResponseType == ResponseType.Success)
                return RedirectToAction("Index");
            else 
            {
                //ValidationError
                foreach (var error in response.ValidationErrors) 
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(workCreateDto);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var response = await _workService.GetByIdAsync<WorkUpdateDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
                return NotFound();
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto workUpdateDto)
        {
            var response = await _workService.Update(workUpdateDto);
            if(response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View(workUpdateDto);
            }
            else if(response.ResponseType == ResponseType.NotFound)
                return NotFound();
            else
                return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var response = await _workService.Remove(id);
            if (response.ResponseType == ResponseType.Success)
                return RedirectToAction("Index");
            else
                return NotFound(); 
        }
    }
}
