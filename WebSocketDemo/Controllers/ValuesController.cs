﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories.Video;
using Microsoft.AspNetCore.Mvc;

namespace WebSocketDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private NotificationsMessageHandler _notificationsMessageHandler { get; set; }
        private readonly IVideoQueryRepository _videoQueryRepository;
        public ValuesController(NotificationsMessageHandler notificationsMessageHandler, IVideoQueryRepository videoQueryRepository)
        {
            _notificationsMessageHandler = notificationsMessageHandler;
            _videoQueryRepository = videoQueryRepository;
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await _notificationsMessageHandler.SendMessageToAllAsync("burası api values get kısmı");
            List<string> lst = new List<string>();
            foreach (var item in _videoQueryRepository.GetAll())
            {
                lst.Add(item.Title);
            }
            
            return lst; 
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
