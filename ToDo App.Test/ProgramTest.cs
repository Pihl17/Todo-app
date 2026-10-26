using NUnit.Framework;
using ToDo_App;

namespace ToDo_App.Test;

[TestFixture]
public class ProgramTest
{
    [Test]
    public void CreateInputManager_ReturnsInputManager()
    {
        var inputManager = Program.CreateInputManager();

        Assert.That(inputManager, Is.Not.Null);
    }
}