﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Apis.Tests.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://localhost:5001/api/Todos";

            using (var client = new HttpClient())
            {
                // 데이터 전송
                var json = JsonConvert.SerializeObject(new Todo { Title = "HttpClient", IsDone = true});
                var post = new StringContent(json, Encoding.UTF8, "application/json");

                await client.PostAsync(url, post);

                // 데이터 수신
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<Todo>>(result);
                foreach (var item in todos)
                {
                    Console.WriteLine($"{item.Id} - {item.Title}({item.IsDone})");
                }
            }

        }
    }
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }

}
