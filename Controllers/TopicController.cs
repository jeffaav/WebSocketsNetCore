using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebSocketsNetCore.Models.Entities;
using WebSocketsNetCore.Models.Mappers;
using WebSocketsNetCore.Models;
using WebSocketsNetCore.ViewModels.Topic;
using WebSocketsNetCore.ViewModels.Shared;

namespace WebSocketsNetCore.Controllers
{
    public class TopicController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public TopicController(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = new MapperConfiguration(config =>
            {
                TopicMappers.RegisterMappings(config);
            })
            .CreateMapper();
        }

        public IActionResult Index() => View(_unitOfWork.TopicRepository.Find());

        public IActionResult New() => View(new NewVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(viewModel.Id))
                    await _unitOfWork.TopicRepository.InsertOne(mapper.Map<Topic>(viewModel));
                else
                    await _unitOfWork.TopicRepository.UpdateOne(viewModel.Id, mapper.Map<Topic>(viewModel));

                ViewBag.Saved = true;
            }

            return View(viewModel);
        }

        public IActionResult Edit(string id) => View("New", mapper.Map<NewVM>(_unitOfWork.TopicRepository.FindOne(id)));

        public IActionResult Delete(string id) => View("Delete", new DeleteVM { Id = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.TopicRepository.DeleteOne(viewModel.Id);
                viewModel.Deleted = true;
            }

            return View(viewModel);
        }
    }
}