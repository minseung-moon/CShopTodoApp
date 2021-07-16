using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CShopTodoApp.Models
{
    public class TodoRepositoryFile : ITodoRepository
    {
        // 파일 경로
        private readonly string _filePath = "";

        // 인-메모리 역할을 해줄 컬렉션 생성
        private static List<Todo> _todos = new List<Todo>();

        public TodoRepositoryFile(string filePath = @"C:\Temp\Todos.txt")
        {
            _filePath = filePath;
            string[] todos = File.ReadAllLines(_filePath, Encoding.Default);
            foreach (var item in todos)
            {
                string[] line = item.Split(',');
                _todos.Add(new Todo { Id = Convert.ToInt32(line[0]), Title = line[1], IsDone = Convert.ToBoolean(line[2]) });
            }
        }

        public void Add(Todo model)
        {
            model.Id = _todos.Max(t => t.Id) + 1;
            _todos.Add(model);

            // 파일 저장
            string data = "";
            foreach(var t in _todos)
            {
                // \r\n == Environment.NewLine
                data += $"{t.Id},{t.Title},{t.IsDone}{Environment.NewLine}";
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    sw.Write(data);
                    sw.Close();  // 파일 닫고
                    // sw.Dispose();  // 메모리 제거
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Todo> GetAll()
        {
            return _todos.ToList();
        }
    }

}