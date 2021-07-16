using System.Collections.Generic;

namespace CShopTodoApp.Models
{
    public interface ITodoRepository
    {
        void Add(Todo model); // 입력
        List<Todo> GetAll(); // 출력
    }

}
