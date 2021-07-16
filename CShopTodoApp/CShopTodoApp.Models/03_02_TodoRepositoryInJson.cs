using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CShopTodoApp.Models
{
    public class TodoRepositoryJson : ITodoRepository
    {
        // 파일 경로
        private readonly string _filePath = "";

        // 인-메모리 역할을 해줄 컬렉션 생성
        private static List<Todo> _todos = new List<Todo>();

        public TodoRepositoryJson(string filePath = @"C:\Temp\Todos.json")
        {
            _filePath = filePath;
            var todos = File.ReadAllText(_filePath, Encoding.Default);
            // DeserializeObject JSON => C# 객체
            _todos = JsonConvert.DeserializeObject<List<Todo>>(todos);
        }

        public void Add(Todo model)
        {
            model.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(model);

            // 파일 저장
            // SerializeObject C# 객체 => JSON
            // Formatting.Indented, 들여쓰기
            string json = JsonConvert.SerializeObject(_todos, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<Todo> GetAll()
        {
            return _todos.ToList();
        }
    }

}