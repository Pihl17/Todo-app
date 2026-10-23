using System;
using System.Collections.Generic;
using System.Text;
using ToDo_App.Library.Models;

namespace ToDo_App.Library.Data
{
    public interface IDataHandler
    {

        /// <summary>
        /// Saves a todo to the data-set
        /// </summary>
        /// <param name="todo">The todo to save</param>
        /// <returns>Result object over the success of the query result</returns>
        DataQueryResult AddTodo(ToDo todo);

        /// <summary>
        /// Removes and deletes a todo from the data-set
        /// </summary>
        /// <param name="todoId">Id of the todo to remove</param>
        /// <returns>Result object over the success of the query result</returns>
        DataQueryResult RemoveTodo(Guid todoId);

        /// <summary>
        /// Updates the todo in the data-set by replacing the exsiting todo with the same Id with the updated one
        /// </summary>
        /// <param name="updatedTodo">The updated todo. It's Id is used to locate the todo to update in the data-set</param>
        /// <returns>Result object over the success of the query result</returns>
        DataQueryResult UpdateTodo(ToDo updatedTodo);

        /// <summary>
        /// Loads a single Todo by Id and returns it.
        /// </summary>
        /// <param name="todoId">Todo Id to look for</param>
        /// <param name="todo">The todo that will be returned</param>
        /// <returns>Result object over the success of the query result</returns>
        DataQueryResult LoadTodo(Guid todoId, out ToDo todo);
        
        /// <summary>
        /// Loads and returns the todo list from the database
        /// </summary>
        /// <param name="todoList">The list of todos that will be returned</param>
        /// <returns>Result object over the success of the query result</returns>
        DataQueryResult LoadTodoList(out List<ToDo> todoList);

    }
}
