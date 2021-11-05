using System;
using System.Collections.Generic;

namespace todo_rest_api
{
    public class Dashboard
    {
        public int? TasksCountForToday { get; set; }
        public List<ListsInfo>? ListWithNotComplitedTasks { get; set; }
    }
    public class ListsInfo
    {
        string ListName { get; set; }
        int NotComlitedCount { get; set; }
    }
}