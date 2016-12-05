using Microsoft.AspNetCore.Mvc;
using WebSocketsNetCore.Models.Repositories;
using WebSocketsNetCore.ViewModels.Topic;

namespace WebSocketsNetCore.Controllers
{
    public class TopicController : Controller
    {
        private readonly TopicRepository topicRepository;

        public TopicController(TopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public IActionResult Index() => View(topicRepository.Find());

        public IActionResult New() => View(new NewVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(NewVM viewModel) 
        {
            if (ModelState.IsValid)
            {
                ViewBag.Saved = true;
            }

            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var topic = topicRepository.FindOne(id);
            return View("New", new NewVM 
            {
                Id = topic._id.ToString(),
                Name = topic.name 
            });
        } 
    }
}