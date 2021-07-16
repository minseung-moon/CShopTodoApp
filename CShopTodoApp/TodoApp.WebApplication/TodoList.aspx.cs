using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TodoApp.WebApplication
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           DisplayData();
        }

        private void DisplayData()
        {
            string url = "https://localhost:5001/api/Todos";

            using (var client = new HttpClient())
            {
                // 데이터 전송
                var json = JsonConvert.SerializeObject(new Todo { Title = "HttpClient", IsDone = true });
                var post = new StringContent(json, Encoding.UTF8, "application/json");

                client.PostAsync(url, post).Wait();

                // 데이터 수신
                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                var todos = JsonConvert.DeserializeObject<List<Todo>>(result);
                foreach (var item in todos)
                {
                    Console.WriteLine($"{item.Id} - {item.Title}({item.IsDone})");
                }

                // 필터링 : LINQ로 함수형 프로그래밍 스타일 구현
                // Select() : map()
                // var q = from todo in todos
                // select todo; // todos.Select(t => t); 랑 동일
                // IEnumerable<Todo> q = todos.Select(t => t); // 여기까지는 내용은 동일
                // var q = from todo in todos
                //  select new TodoViewModel { Title = todo.Title, IsDone = todo.IsDone };
                var query = todos.AsQueryable<Todo>(); // query 형태로 계속 출력하고 싶을 때

                // 조건 처리
                if(DateTime.Now.Second % 2 == 0)
                {
                    query = query.Where(qr => qr.Id % 2 == 0); // 조건절 추가(짝수)
                }else
                {
                    query = query.Where(qr => qr.Id % 2 != 0); // 조건절 추가(홀수)
                }

                // 조건처리
                query = query.Where(it => it.IsDone == false);

                // 정렬 처리
                const string sortOrder = "Title";                
                query = (sortOrder == "Title" ? query.OrderBy(it => it.Title) : query);
                
                var q = query.Select(t => new TodoViewModel { Title = t.Title, IsDone = t.IsDone}); // Todo 타입을  새로운 TodoViewModel로 맵핑해서 q에 저장
                

                // 데이터 바인딩
                this.GridView1.DataSource = q;
                this.GridView1.DataBind();

                this.GridView2.DataSource = todos.Where(t => t.Id % 2 == 1 && t.IsDone == false)
                    .OrderByDescending(t => t.Title)
                    .Select(t => new { 제목 = t.Title , 완료여부 = t.IsDone})
                    .ToList();
                this.GridView2.DataBind();
            }
        }
    }

    public class TodoViewModel
    {

        public string Title { get; set; }
        public bool IsDone { get; set; }
    }

    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}