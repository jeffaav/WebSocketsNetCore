using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebSocketsNetCore.Models.Entities;
using WebSocketsNetCore.Models.Mappers;
using WebSocketsNetCore.Models.Repositories;
using WebSocketsNetCore.ViewModels.Topic;

namespace WebSocketsNetCore.Controllers
{
    public class TopicController : Controller
    {
        private readonly TopicRepository topicRepository;
        private readonly IMapper mapper;

        public TopicController(TopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
            this.mapper = new MapperConfiguration(config => { TopicMappers.RegisterMappings(config); }).CreateMapper();
        }

        public IActionResult Index() => View(topicRepository.Find());

        public IActionResult New() => View(new NewVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewVM viewModel) 
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(viewModel.Id)) {
                    await topicRepository.Insert(mapper.Map<Topic>(viewModel));
                } else {

                }
                ViewBag.Saved = true;
            }

            return View(viewModel);
        }

        public IActionResult Edit(string id) => View("New", mapper.Map<NewVM>(topicRepository.FindOne(id)));
    }
}