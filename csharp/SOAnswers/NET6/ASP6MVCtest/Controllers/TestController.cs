using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace ASP6MVCtest.Controllers;

public class TestController: Controller
{
    //Create a new note
    [HttpGet("[controller]/[action]")]
    public ActionResult EditNote()
    {
        return View(new Model()
        {
            Id = 12
        });
    }

    //Edit a selected note
    [HttpGet]
    public ActionResult EditNote(int id)
    {
        var model = new Model()
        {
            Id = id
        };
        return View(model);
    }
}

public class Model
{
    public int Id { get; set; }
} 

public interface IDataDictionary
{
    public List<T> GetAllItemsFromList<T>();
    public T GetSingleItemFromList<T>(int id) where T : DataElement;
}

public class DataDictionary : IDataDictionary
{
    public List<IList> Data = new List<IList>();

    // Return all objects from the list of type T
    public List<T> GetAllItemsFromList<T>()
    {
        return Data.OfType<T>().ToList(); // This works, returning the appropriate list.
    }

    // Return specific object from the list of type T by Id property value
    public T GetSingleItemFromList<T>(int id) where T : DataElement
    {
        List<T> list = Data.OfType<List<T>>().First(); // This works, resolving to the correct list (e.g. Courses).
        return list.Where(i => i.Id == id)
            .First(); // This doesn't work. It doesn't appear to know about the Id property in this context.
    }
    
    public T GetSingleItemFromList<T>(int id, Func<T, int> idSelector)
    {
        List<T> list = Data.OfType<List<T>>().First(); 
        return list.First(i => idSelector(i) == id);
    }
}

public interface IHaveId
{
    int Id { get; set; }
}

public abstract class DataElement
{
    public int Id { get; set; }
}